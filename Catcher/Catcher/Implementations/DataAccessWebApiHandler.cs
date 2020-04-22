using System;
using System.Net.Http;
using System.Threading.Tasks;

using Catcher.Configurations;
using Catcher.Interfaces;

using Microsoft.Extensions.Options;

namespace Catcher.Implementations
{
	internal class DataAccessWebApiHandler : IDataHandler
	{
		private readonly Uri _putRoute;
		private readonly string _formValueName;
		private readonly HttpClient _httpClient;

		public DataAccessWebApiHandler(IOptions<DataAccessWebApiConfig> options)
		{
			var config = options.Value;

			_putRoute = new Uri(config.Host + config.PutRoute);
			_formValueName = config.FormParamName;
			_httpClient = new HttpClient();
		}

		public async Task SaveAsync(string data)
		{
			var content = GetContent(data);
			await _httpClient.PutAsync(_putRoute, content);
		}

		public async Task<bool> TrySaveAsync(string data)
		{
			var content = GetContent(data);
			var response = await _httpClient.PutAsync(_putRoute, content);

			return response.IsSuccessStatusCode;
		}

		private HttpContent GetContent(string data) =>
			new MultipartFormDataContent
				{
						{ new StringContent(data), _formValueName },
				};
	}
}
