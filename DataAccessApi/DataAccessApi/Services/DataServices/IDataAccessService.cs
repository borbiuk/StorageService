using API.TransferData;
using System.Threading.Tasks;

namespace API.Services.DataServices
{
	public interface IDataAccessService
	{
		Task<EntityDto> GetDataAsync(long id);

		Task RemoveDataAsync(long id);

		Task<long> SaveDataAsync(string data);

		Task UpdateDataAsync(UpdateEntityDto dto);
	}
}
