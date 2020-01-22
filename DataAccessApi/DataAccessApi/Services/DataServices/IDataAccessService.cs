using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;
using DataAccess.Entities;

namespace API.Services.DataServices
{
	public interface IDataAccessService
	{
		Task<UserEntity> GetUserAsync(long id);

		Task RemoveUserAsync(long id);

		Task<long> SaveUserAsync(string name);

		Task SetUserSoftware(long userId, long softwareId);

		Task RemoveUserSoftware(long userId, long softwareId);

		Task<SoftwareEntity> GetSoftwareAsync(long id);

		Task RemoveSoftwareAsync(long id);

		Task<long> SaveSoftwareAsync(string name);

		Task<IEnumerable<long>> GetSoftwareOwners(long softwareId);
	}
}
