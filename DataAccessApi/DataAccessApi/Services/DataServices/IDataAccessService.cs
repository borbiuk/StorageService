using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.API.Dto;
using DataAccess.Entities;

namespace API.Services.DataServices
{
	public interface IDataAccessService
	{
		Task<UserDto> GetUserAsync(long id);

		Task RemoveUserAsync(long id);

		Task<long> SaveUserAsync(string name);

		Task SetUserSoft(long userId, long softId);

		Task RemoveUserSoft(long userId, long softId);

		Task<SoftEntity> GetSoftAsync(long id);

		Task RemoveSoftAsync(long id);

		Task<long> SaveSoftAsync(string name);

		Task<IEnumerable<long>> GetSoftOwners(long softId);
	}
}
