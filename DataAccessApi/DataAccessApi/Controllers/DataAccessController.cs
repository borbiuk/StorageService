using API.Services;
using API.TransferData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("api/data")]
	public class DataAccessController : ControllerBase
	{
		private const long ErrorIdValue = -1;
		private const object ErrorDtoValue = null;

		private readonly IDataAccessService _das;
		private readonly ILogger _logger;

		public DataAccessController(
			IDataAccessService dataAccessService, ILogger<DataAccessController> logger)
		{
			_das = dataAccessService;
			_logger = logger;
		}

		[HttpDelete]
		[Route("delete/{id}")]
		public async Task DeleteData([FromRoute]long id)
		{
			try
			{
				await _das.RemoveDataAsync(id);
			}
			catch (Exception ex)
			{
				LogException(ex);
			}
		}

		[HttpGet]
		[Route("get/{id}")]
		public async Task<EntityDto> GetData([FromRoute]long id)
		{
			try
			{
				return await _das.GetDataAsync(id);
			}
			catch (Exception ex)
			{
				LogException(ex);
				return (EntityDto)ErrorDtoValue;
			}
		}

		[HttpPut]
		[Route("create")]
		public async Task<long> SaveData([FromForm]string data)
		{
			try
			{
				return await _das.SaveDataAsync(data);
			}
			catch (Exception ex)
			{
				LogException(ex);
				return ErrorIdValue;
			}
		}

		[HttpPost]
		[Route("update")]
		public async Task UpdateData([FromForm]EntityDto dto)
		{
			try
			{
				await _das.UpdateDataAsync(dto);
			}
			catch (Exception ex)
			{
				LogException(ex);
			}
		}

		private void LogException(Exception exception) => _logger.LogError($"[{DateTime.Now}] - {exception}");
	}
}
