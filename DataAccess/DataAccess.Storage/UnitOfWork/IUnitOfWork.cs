using DAL.Entities;
using DAL.Repositories;

using System;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
	public interface IUnitOfWork
	{
		Task CommitAsync();

		IRepository<DataEntity> Entities { get; }
	}
}
