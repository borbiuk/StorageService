using DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
	public interface IRepository<TEntity> where TEntity : class, IEntity
	{
		void AddOrUpdate(TEntity entity);

		Task AddOrUpdateAsync(TEntity entity);

		void Delete(long id);

		Task DeleteAsync(long id);

		TEntity Get(long id);

		Task<TEntity> GetAsync(long id);

		IQueryable<TEntity> GetAll();
	}
}
