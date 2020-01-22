using DAL.Entities;
using DAL.Repositories;
using System;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DAL.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
		Task CommitAsync();

		IRepository<UserEntity> Users { get; }

		IRepository<SoftwareEntity> Software { get; }

		IRepository<UserSoftwareEntity> UserSoftware { get; }
	}
}
