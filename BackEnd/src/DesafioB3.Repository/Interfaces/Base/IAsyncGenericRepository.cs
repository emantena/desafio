using System.Linq.Expressions;

namespace DesafioB3.Repository.Interfaces.Base;

public interface IAsyncGenericRepository<TDbContext, TEntity>
{
	Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

	Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

	Task<TEntity> SingleOrDefaultAsync(
		Expression<Func<TEntity, bool>> predicate,
		CancellationToken cancellationToken = default);

	Task<TEntity> FirstOrDefaultAsync(
		Expression<Func<TEntity, bool>> predicate,
		CancellationToken cancellationToken = default);

	Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
		Expression<Func<TEntity, object>> keySelector,
		CancellationToken cancellationToken = default);

	Task<IEnumerable<TEntity>> SearchAsync(
		Expression<Func<TEntity, bool>> predicate,
		CancellationToken cancellationToken = default);

	Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default);

	Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default);

	Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess = true, CancellationToken cancellationToken = default);
}