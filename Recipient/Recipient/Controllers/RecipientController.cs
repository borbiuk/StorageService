using System.Threading.Tasks;

using BackgroundProvider;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using QueueClient;

namespace Recipient.Controllers
{
	[ApiController]
	[Route("api")]
	public class RecipientController : ControllerBase
	{
		private readonly string _queueName;
		private readonly BackgroundWorkerProvider _backgroundWorker;
		private readonly IQueueClient _queueClient;

		public RecipientController(IConfiguration configuration,
			BackgroundWorkerProvider backgroundWorker,
			IQueueClient queueClient)
		{
			_queueName = configuration["RabbitMq:QueueName"];
			_backgroundWorker = backgroundWorker;
			_queueClient = queueClient;
		}

		[HttpPut]
		[Route("save")]
		public async Task Save([FromForm]string data)
		{
			var sendDataToQueue = _queueClient.SendAsync(_queueName, data);
			await _backgroundWorker.ToQueue(async () => await  sendDataToQueue);
		}
	}
}
