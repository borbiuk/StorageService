using DAL.Entities;
using DAL.Repositories;

using System;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
		void Commit();

		Task CommitAsync();

		IRepository<SimpleEntity> Entities { get; }
	}
}
