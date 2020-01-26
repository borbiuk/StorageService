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
		public async Task RemoveSoft([FromRoute]long id) => await _das.RemoveSoftAsync(id);

		[HttpGet]
		[Route("get/{id}")]
		public async Task<SoftEntity> GetSoft([FromRoute]long id) => await _das.GetSoftAsync(id);

		[HttpPut]
		[Route("create")]
		public async Task<long> CreateSoft([FromForm]string data) => await _das.SaveSoftAsync(data);

		[HttpGet]
		[Route("owners/{id}")]
		public async Task<IEnumerable<long>> GetOwners([FromForm] long id) => await _das.GetSoftOwners(id);
	}
}
