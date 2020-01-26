using System.Threading.Tasks;
using API.Services.DataServices;
using DAL.Entities;
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
		public async Task RemoveUser([FromRoute]long id) => await _das.RemoveUserAsync(id);

		[HttpGet]
		[Route("get/{id}")]
		public async Task<UserDto> GetUser([FromRoute]long id) => await _das.GetUserAsync(id);

		[HttpPut]
		[Route("create")]
		public async Task<long> CreateUser([FromForm]string data) => await _das.SaveUserAsync(data);

		[HttpPost]
		[Route("set")]
		public async Task SetSoft([FromForm] long userId, long softwareId) =>
			await _das.SetUserSoft(userId, softwareId);

		[HttpPost]
		[Route("take")]
		public async Task TakeSoft([FromForm] long userId, [FromForm] long softwareId) =>
			await _das.RemoveUserSoft(userId, softwareId);
	}
}
