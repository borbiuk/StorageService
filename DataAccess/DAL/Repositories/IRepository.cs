using DAL.Entities;

using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
	public interface IRepository<TEntity> where TEntity : class, IEntity
	{
		Task AddOrUpdate(TEntity entity);

		Task Delete(long id);

		Task<TEntity> Get(long id);

		IQueryable<TEntity> GetAll();
	}
}
