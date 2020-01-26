using DAL.Entities;
using DAL.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.API.Dto;
using DataAccess.Entities;

namespace API.Services.DataServices
{
	internal class DataAccessService : IDataAccessService
	{
		private readonly IUnitOfWork _uow;

		public DataAccessService(IUnitOfWork unitOfWork)
		{
			_uow = unitOfWork;
		}

		public async Task<UserDto> GetUserAsync(long id)
		{
			var user = await _uow.Users.Get(id);
			if (user == null)
				return null;

			var userSoft = _uow.UserSoft.GetAll()
				.Where(x => x.UserId == id)
				.Select(x => x.SoftId);

			return new UserDto
			{
				Id = user.Id,
				Name = user.Name,
				Soft = userSoft.AsEnumerable() ?? Enumerable.Empty<long>()
			};
		}

		public async Task RemoveUserAsync(long id)
		{
			await _uow.Users.Delete(id);
			await _uow.CommitAsync();
		}

		public async Task<long> SaveUserAsync(string name)
		{
			var entity = new UserEntity
			{
				Name = name,
			};

			await _uow.Users.AddOrUpdate(entity);
			await _uow.CommitAsync();

			return entity.Id;
		}

		public async Task SetUserSoft(long userId, long softId)
		{
			var soft = await GetSoftAsync(softId);
			if (soft == null)
				return;

			var user = await _uow.Users.Get(userId);
			if (user == null)
				return;

			if (user.UserSoftEntities == null || !user.UserSoftEntities.Any())
				user.UserSoftEntities = new List<UserSoftEntity>();
			else if (user.UserSoftEntities.Any(x => x.SoftId == softId))
				return;

			var userSoft = new UserSoftEntity
			{
				UserId = userId,
				User = user,
				SoftId = softId,
				Soft = soft
			};

			await _uow.UserSoft.AddOrUpdate(userSoft);
			await _uow.CommitAsync();
		}

		public async Task RemoveUserSoft(long userId, long softId)
		{
			//var software = await GetSoftAsync(softwareId);
			//if (software == null)
			//	return;
			//
			//var user = await GetUserAsync(userId);
			//if (user?.UserSoftwareEntities == null
			//	|| user.UserSoftwareEntities.Count == 0
			//	|| user.UserSoftwareEntities.FirstOrDefault(x => x.SoftwareId == softwareId) == null)
			//	return;
			//
			//user.UserSoftwareEntities.Remove(user.UserSoftwareEntities.First(x => x.SoftwareId == softwareId));
			//
			//await _uow.Users.AddOrUpdate(user);
			//await _uow.CommitAsync();
		}

		public async Task<SoftEntity> GetSoftAsync(long id) => await _uow.Soft.Get(id);

		public async Task RemoveSoftAsync(long id)
		{
			await _uow.Soft.Delete(id);
			await _uow.CommitAsync();
		}

		public async Task<long> SaveSoftAsync(string name)
		{
			var entity = new SoftEntity()
			{
				Name = name,
			};
			await _uow.Soft.AddOrUpdate(entity);
			await _uow.CommitAsync();

			return entity.Id;
		}

		public async Task<IEnumerable<long>> GetSoftOwners(long softwareId)
		{
			var software = await GetSoftAsync(softwareId);
			if (software == null)
				return null;

			var list = _uow.UserSoft.GetAll()
				.Where(x => x.SoftId == softwareId)
				.Select(x => x.UserId);

			return list;
		}
	}
}
