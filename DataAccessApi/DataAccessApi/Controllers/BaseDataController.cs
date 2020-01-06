using API.Services;
using API.TransferData;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BaseDataController : ControllerBase
	{
		private readonly IDataAccessService _das;

		public BaseDataController(IDataAccessService dataAccessService)
		{
			_das = dataAccessService;
		}

		[HttpGet]
		public BaseEntityDto Get(long id)
		{
			return _das.GetData(id);
		}

		[HttpPost]
		[Route("Save")]
		public async Task<long> SaveData([FromForm]string data)
		{
			if (string.IsNullOrWhiteSpace(data))
				return 666;

			var result = await _das.SaveDataAsync(data);

			return result;
		}
	}
}
