using System.Collections.Generic;
using API.Services.DataServices;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Api.Controllers
{
	[ApiController]
	[Route("api/soft")]
	public class SoftController : ControllerBase
	{
		private readonly IDataAccessService _das;

		public SoftController(IDataAccessService dataAccessService)
		{
			_das = dataAccessService;
		}

		[HttpDelete]
		[Route("delete/{id}")]
		public async Task<ActionResult> RemoveSoft([FromRoute]long id)
		{
			await _das.RemoveSoftAsync(id);
			return StatusCode(200);
		}

		[HttpGet]
		[Route("get/{id}")]
		public async Task<ActionResult<SoftEntity>> GetSoft([FromRoute]long id) => await _das.GetSoftAsync(id);

		[HttpPut]
		[Route("create")]
		public async Task<ActionResult<long>> CreateSoft([FromHeader]string data) => await _das.SaveSoftAsync(data);

		[HttpGet]
		[Route("owners/{id}")]
		public async Task<ActionResult<IEnumerable<long>>> GetOwners([FromRoute] long id)
		{
			var result = await _das.GetSoftOwners(id);
			return new ActionResult<IEnumerable<long>>(result);
		}
	}
}
