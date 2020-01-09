using API.Services.DataServices;
using API.TransferData;
using Microsoft.AspNetCore.Http;
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
		private readonly IHttpContextAccessor _accessor;
		private readonly ILogger _logger;

		public DataAccessController(
			IDataAccessService dataAccessService, IHttpContextAccessor accessor, ILogger<DataAccessController> logger)
		{
			_das = dataAccessService;
			_accessor = accessor;
			_logger = logger;
		}

		[HttpDelete]
		[Route("delete/{id}")]
		public async Task DeleteData([FromRoute]long id)
		{
			var x = GetId();
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
			var x = GetId();
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
			var x = GetId();
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
		public async Task UpdateData([FromForm]UpdateEntityDto dto)
		{
			var x = GetId();
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

		private string GetId() => _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
	}
}
