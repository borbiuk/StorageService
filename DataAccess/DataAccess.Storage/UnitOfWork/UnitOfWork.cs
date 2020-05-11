using DAL.Entities;
using DAL.Repositories;

using System;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork, IDisposable
	{
		private readonly ApplicationContext _context;

		private IRepository<DataEntity> _entities;
		private bool _disposed;

		public UnitOfWork(ApplicationContext appContext)
		{
			_context = appContext;
		}

		/// <summary>
		/// Repository provide access to <see cref="DataEntity"/> storage.
		/// </summary>
		public IRepository<DataEntity> Entities =>
			_entities
			?? (_entities = new GenericRepository<DataEntity>(_context));

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
