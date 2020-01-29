using System.Collections.Generic;
using System.Threading.Tasks;
using API.Services.DataServices;
using DataAccess.API.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DataAccess.API.Controllers
{
	[ApiController]
	[Route("api/user")]
	public class UserController : ControllerBase
	{
		private readonly IDataAccessService _das;

		public UserController(IDataAccessService dataAccessService)
		{
			_das = dataAccessService;
		}

		[HttpDelete]
		[Route("delete/{id}")]
		public async Task<ActionResult> RemoveUser([FromRoute]long id)
		{
			await _das.RemoveUserAsync(id);
			return StatusCode(200);
		}

		[HttpGet]
		[Route("get/{id}")]
		public async Task<ActionResult<UserDto>> GetUser([FromRoute]long id) => await _das.GetUserAsync(id);

		[HttpPut]
		[Route("create")]
		public async Task<ActionResult<long>> CreateUser([FromHeader]string data) => await _das.SaveUserAsync(data);

		[HttpPost]
		[Route("set")]
		public async Task<ActionResult> SetSoft([FromHeader] long userId, long softId)
		{
			await _das.SetUserSoft(userId, softId);
			return StatusCode(200);
		}

		[HttpPost]
		[Route("take")]
		public async Task<ActionResult> TakeSoft([FromHeader] long userId, [FromHeader] long softId)
		{
			await _das.RemoveUserSoft(userId, softId);
			return StatusCode(200);
		}

		[HttpGet]
		[Route("get/all")]
		public async Task<IEnumerable<UserSimpleDto>> GetAll() => _das.GetAllUsers();
	}
}
