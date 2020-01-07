using API.TransferData;
using System.Threading.Tasks;

namespace API.Services
{
	public interface IDataAccessService
	{
		Task<EntityDto> GetDataAsync(long id);

		Task RemoveDataAsync(long id);

		Task<long> SaveDataAsync(string data);

		Task UpdateDataAsync(EntityDto dto);
	}
}
