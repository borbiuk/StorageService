using DAL.Entities;
using DAL.Repositories;
using System;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
		IRepository<EntityBase> Entities { get; }

		void Commit();

		Task CommitAsync();
	}
}
