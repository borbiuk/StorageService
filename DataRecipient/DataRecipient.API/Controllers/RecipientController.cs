using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DataRecipient.API.Controllers
{
	[ApiController]
	[Route("api/recipient")]
	public class RecipientController : ControllerBase
	{
		private readonly ILogger<RecipientController> _logger;

		public RecipientController(ILogger<RecipientController> logger)
		{
			_logger = logger;
		}

		[HttpPut]
		[Route("send")]
		public async Task SendMessage([FromRoute]string data)
		{
			_logger.LogInformation($"[{DateTime.Now}] - {data}\n\t was receive.");
		}

		[HttpPut]
		[Route("send")]
		public async Task SendMessages([FromForm]ICollection<string> data)
		{
			_logger.LogInformation($"[{DateTime.Now}] - {data}\n\t was receive.");
		}
	}
}
