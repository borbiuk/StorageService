using System.Collections.Generic;
using API.Services.DataServices;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Api.Controllers
{
	[ApiController]
	[Route("api/soft")]
	public class SoftwareController : ControllerBase
	{
		private readonly IDataAccessService _das;

		public SoftwareController(IDataAccessService dataAccessService)
		{
			_das = dataAccessService;
		}

		[HttpDelete]
		[Route("delete/{id}")]
		public async Task RemoveSoftware([FromRoute]long id) => await _das.RemoveSoftwareAsync(id);

		[HttpGet]
		[Route("get/{id}")]
		public async Task<SoftwareEntity> GetSoftware([FromRoute]long id) => await _das.GetSoftwareAsync(id);

		[HttpPut]
		[Route("create")]
		public async Task<long> CreateSoftware([FromForm]string data) => await _das.SaveSoftwareAsync(data);

		[HttpGet]
		[Route("owners/{id}")]
		public async Task<IEnumerable<long>> GetOwners([FromForm] long id) => await _das.GetSoftwareOwners(id);
	}
}
