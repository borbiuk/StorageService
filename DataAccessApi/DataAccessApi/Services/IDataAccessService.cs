using API.TransferData;
using System.Threading.Tasks;

namespace API.Services
{
	public interface IDataAccessService
	{
		BaseEntityDto GetData(long id);

		Task<BaseEntityDto> GetDataAsync(long id);

		void RemoveData(long id);

		Task RemoveDataAsync(long id);

		long SaveData(string data);

		Task<long> SaveDataAsync(string data);

		void UpdateData(BaseEntityDto dto);

		Task UpdateDataAsync(BaseEntityDto dto);
	}
}
