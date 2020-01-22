using DAL.Entities;
using DAL.Repositories;
using System;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DAL.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationContext _context;

		private IRepository<UserEntity> _userEntities;
		private IRepository<SoftwareEntity> _softwareEntities;
		private IRepository<UserSoftwareEntity> _userSoftware;
		private bool _disposed;

		public UnitOfWork(ApplicationContext appContext)
		{
			_context = appContext;
		}

		public IRepository<UserEntity> Users => _userEntities ??= new GenericRepository<UserEntity>(_context);
		
		public IRepository<SoftwareEntity> Software => _softwareEntities ??= new GenericRepository<SoftwareEntity>(_context);

		public IRepository<UserSoftwareEntity> UserSoftware =>
			_userSoftware ??= new GenericRepository<UserSoftwareEntity>(_context);

		/// <summary>
		/// Async commit changes to data storage.
		/// </summary>
		public async Task CommitAsync() => await _context.SaveChangesAsync();

		/// <summary>
		/// Destructor.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed && disposing)
				_context.Dispose();

			_disposed = true;
		}
	}
}
