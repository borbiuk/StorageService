using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using QueueClient;

using Recipient.Services.Interfaces;

namespace Recipient.Controllers
{
	[ApiController]
	[Route("api")]
	internal class RecipientController : ControllerBase
	{
		private readonly string _queueName;
		private readonly IBackgroundWorkerProvider _backgroundWorker;
		private readonly IQueueClient _queueClient;

		internal RecipientController(IConfiguration configuration,
			IBackgroundWorkerProvider backgroundWorker,
			IQueueClient queueClient)
		{
			_queueName = configuration["RabbitMq:QueueName"];
			_backgroundWorker = backgroundWorker;
			_queueClient = queueClient;
		}

		[HttpPut]
		[Route("save")]
		internal async Task Save([FromForm]string data) =>
			await _backgroundWorker.QueueBackgroundAsyncWorkItem(async () =>
				await _queueClient.SendAsync(_queueName, data));
	}
}
