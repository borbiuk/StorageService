using DAL.Entities;

using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
	public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
	{
		private readonly ApplicationContext _context;
		private readonly DbSet<TEntity> _set;

		public GenericRepository(ApplicationContext appContext)
		{
			_context = appContext;
			_set = _context.Set<TEntity>();
		}


		/// <summary>
		/// Save <see cref="TEntity"/> to databese async.
		/// </summary>
		/// <remarks>
		/// If entity id == 0 a new entity is created.
		/// </remarks>
		public async Task AddOrUpdate(TEntity entity)
		{
			if (entity == null)
				throw new ArgumentNullException();

			if (entity.Id == 0)
				await _set.AddAsync(entity);
			else
			{
				var existingEntity = await Get(entity.Id);
				var attachedEntry = _context.Entry(existingEntity);
				attachedEntry.CurrentValues.SetValues(entity);
			}
		}

		/// <summary>
		/// Async remove <see cref="TEntity"/> by id.
		/// </summary>
		public async Task Delete(long id)
		{
			var existingEntity = await Get(id);

			if (existingEntity != null)
				_context.Entry(existingEntity).State = EntityState.Deleted;
		}

		/// <summary>
		/// Async returns <see cref="TEntity"/> instance by id.
		/// </summary>
		public async Task<TEntity> Get(long id) => await _set.FindAsync(id);

		/// <summary>
		/// Returns queryable <see cref="TEntity"/> collection.
		/// </summary>
		public IQueryable<TEntity> GetAll() => _set.AsNoTracking();
	}
}
