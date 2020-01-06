using API.Services;
using API.TransferData;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("api/data")]
	public class DataAccessController : ControllerBase
	{
		private readonly IDataAccessService _das;

		public DataAccessController(IDataAccessService dataAccessService)
		{
			_das = dataAccessService;
		}

		[HttpDelete]
		[Route("delete/{id}")]
		public async Task DeleteData([FromRoute]long id)
		{
			await _das.RemoveDataAsync(id);
		}

		[HttpGet]
		[Route("get/{id}")]
		public BaseEntityDto GetData([FromRoute]long id)
		{
			return _das.GetData(id);
		}

		[HttpPut]
		[Route("create")]
		public async Task<long> SaveData([FromForm]string data)
		{
			return await _das.SaveDataAsync(data);
		}

		[HttpPost]
		[Route("update")]
		public async Task UpdateData([FromForm]BaseEntityDto dto)
		{
			await _das.UpdateDataAsync(dto);
		}
	}
}
