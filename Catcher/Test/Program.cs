using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Test
{
	class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			var service = new X("https://localhost:5003");

			var res = await service.TrySend("Цьомик!");

			Console.WriteLine($"Fuck you == {res}");
		}

		private class X
		{

			private const string PostPath = "/api/data/create";
			private readonly HttpClient _apiClient;

			public X(string apiBaseAddress)
			{
				_apiClient = new HttpClient()
				{
					BaseAddress = new Uri(apiBaseAddress)
				};
			}

			public async Task Send(string data)
			{
				var content = GetStreamContent(data);

				await _apiClient.PutAsync(_apiClient.BaseAddress + PostPath, content);
			}

			public async Task<bool> TrySend(string data)
			{
				try
				{
					var content = GetStreamContent(data);

					var response = await _apiClient.PutAsync(PostPath, content);

					return response.IsSuccessStatusCode;
				}
				catch (HttpRequestException ex)
				{
					return false;
				}
			}

			private static StreamContent GetStreamContent(string data)
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
}
