using DAL.Entities;
using DAL.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

		public async Task<UserEntity> GetUserAsync(long id)
		{
			var user = await _uow.Users.Get(id);
			user.UserSoftwareEntities = _uow.UserSoftware.GetAll().Where(_ => _.UserId == id).ToList();

			return user;
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

		public async Task SetUserSoftware(long userId, long softwareId)
		{
			var software = await GetSoftwareAsync(softwareId);
			if (software == null)
				return;

			var user = await GetUserAsync(userId);
			if (user == null)
				return;

			if (user.UserSoftwareEntities == null)
				user.UserSoftwareEntities = new List<UserSoftwareEntity>();

			user.UserSoftwareEntities.Add(new UserSoftwareEntity
			{
				UserId = userId,
				User = user,
				SoftwareId = softwareId,
				Software = software,
			});

			await _uow.Users.AddOrUpdate(user);
			await _uow.CommitAsync();
		}

		public async Task RemoveUserSoftware(long userId, long softwareId)
		{
			var software = await GetSoftwareAsync(softwareId);
			if (software == null)
				return;

			var user = await GetUserAsync(userId);
			if (user?.UserSoftwareEntities == null
				|| user.UserSoftwareEntities.Count == 0
				|| user.UserSoftwareEntities.FirstOrDefault(x => x.SoftwareId == softwareId) == null)
				return;

			user.UserSoftwareEntities.Remove(user.UserSoftwareEntities.First(x => x.SoftwareId == softwareId));

			await _uow.Users.AddOrUpdate(user);
			await _uow.CommitAsync();
		}

		public async Task<SoftwareEntity> GetSoftwareAsync(long id) => await _uow.Software.Get(id);

		public async Task RemoveSoftwareAsync(long id)
		{
			await _uow.Software.Delete(id);
			await _uow.CommitAsync();
		}

		public async Task<long> SaveSoftwareAsync(string name)
		{
			var entity = new SoftwareEntity()
			{
				Name = name,
			};
			await _uow.Software.AddOrUpdate(entity);
			await _uow.CommitAsync();

			return entity.Id;
		}

		public async Task<IEnumerable<long>> GetSoftwareOwners(long softwareId)
		{
			var software = await GetSoftwareAsync(softwareId);
			if (software == null)
				return null;

			var list = _uow.UserSoftware.GetAll()
				.Where(x => x.SoftwareId == softwareId)
				.Select(x => x.UserId);

			return list;
		}
	}
}
