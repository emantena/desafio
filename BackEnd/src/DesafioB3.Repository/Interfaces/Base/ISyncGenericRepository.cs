using System.Linq.Expressions;

namespace DesafioB3.Repository.Interfaces.Base;

public interface ISyncGenericRepository<TDbContext, TEntity>
{
	TEntity Add(TEntity entity);

	void AddRange(IEnumerable<TEntity> entities);

	void Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties);

	void UpdateRange(IEnumerable<TEntity> entities);

	void RemoveByEntity(TEntity entity);

	void RemoveSingle(Expression<Func<TEntity, bool>> predicate);

	void RemoveRange(IEnumerable<TEntity> entities);

	TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

	TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

	TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> keySelector);

	IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate);

	bool Any(Expression<Func<TEntity, bool>> predicate = null);

	long Count(Expression<Func<TEntity, bool>> predicate = null);


	int SaveChanges(bool acceptAllChangesOnSuccess = true);
}