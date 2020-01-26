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
		private IRepository<SoftEntity> _softEntities;
		private IRepository<UserSoftEntity> _userSoft;
		private bool _disposed;

		public UnitOfWork(ApplicationContext appContext)
		{
			_context = appContext;
		}

		public IRepository<UserEntity> Users => _userEntities ??= new GenericRepository<UserEntity>(_context);
		
		public IRepository<SoftEntity> Soft => _softEntities ??= new GenericRepository<SoftEntity>(_context);

		public IRepository<UserSoftEntity> UserSoft =>
			_userSoft ??= new GenericRepository<UserSoftEntity>(_context);

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
