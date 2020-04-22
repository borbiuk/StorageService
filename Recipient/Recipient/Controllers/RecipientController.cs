using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using BackgroundProvider;
using QueueClient;

namespace Recipient.Controllers
{
	[ApiController]
	[Route("api")]
	public class RecipientController : ControllerBase
	{
		private readonly BackgroundWorkerProvider _backgroundWorker;
		private readonly IQueueClient _queueClient;

		public RecipientController(IQueueClient queueClient,
			BackgroundWorkerProvider backgroundWorker)
		{
			_backgroundWorker = backgroundWorker;
			_queueClient = queueClient;
		}

		[HttpPut]
		[Route("save")]
		public async Task Save([FromForm]string data)
		{
			await _backgroundWorker.ToQueue(async () =>
				await _queueClient.SendAsync(data));
		}
	}
}
