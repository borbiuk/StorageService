using DAL.Entities;
using DAL.Repositories;
using System;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppContext _context;

		private IRepository<EntityBase> _entities;
		private bool _disposed;

		public UnitOfWork(AppContext appContext)
		{
			_context = appContext;
		}

		/// <summary>
		/// Repository provide access to <see cref="EntityBase"/> storage.
		/// </summary>
		public IRepository<EntityBase> Entities => _entities ?? (_entities = new GenericRepository<EntityBase>(_context));

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
