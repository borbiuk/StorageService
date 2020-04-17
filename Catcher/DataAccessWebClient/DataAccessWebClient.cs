using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Catcher.Interfaces;

namespace DataAccessWebClient
{
	public class DataAccessWebClient : IDataAccessClient
	{
		private const string PostPath = "";
		private readonly HttpClient _apiClient;

		public DataAccessWebClient(string apiBaseAddress)
		{
			_apiClient = new HttpClient
			{
				BaseAddress = new Uri(apiBaseAddress)
			};
		}

		public async Task Send(string data)
		{
			var content = GetStreamContent(data);

			await _apiClient.PostAsync(PostPath, content);
		}

		public async Task<bool> TrySend(string data)
		{
			throw new System.NotImplementedException();
		}

		private static HttpContent GetStreamContent(string data)
		{
			using var stream = GetStream(data);
			return new StreamContent(stream);
		}

		private static Stream GetStream(string data)
		{
			var stream = new MemoryStream();
			var writer = new StreamWriter(stream);
			
			writer.Write(data);
			writer.Flush();
			stream.Position = 0;
			
			return stream;
		}
	}
}
