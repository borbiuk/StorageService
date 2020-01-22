using DAL.Entities;
using DAL.Repositories;
using System;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationContext _context;

		private IRepository<SimpleEntity> _entities;
		private bool _disposed;

		public UnitOfWork(ApplicationContext appContext)
		{
			_context = appContext;
		}

		/// <summary>
		/// Repository provide access to <see cref="SimpleEntity"/> storage.
		/// </summary>
		public IRepository<SimpleEntity> Entities => _entities ?? (_entities = new GenericRepository<SimpleEntity>(_context));

		/// <summary>
		/// Commit changes to data storage.
		/// </summary>
		public void Commit() => _context.SaveChanges();

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
