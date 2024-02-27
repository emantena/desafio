using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DesafioB3.Repository.Base;
public sealed partial class GenericRepository<TDbContext, TEntity>
{
	public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
	{
		await DbSet.AddAsync(entity, cancellationToken);

		await SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken);

		return entity;
	}

	public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
	{
		await DbSet.AddRangeAsync(entities, cancellationToken);

		await SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken);
	}

	public async Task<bool> AnyAsync(
		Expression<Func<TEntity, bool>> predicate = null,
		CancellationToken cancellationToken = default)
	{
		return predicate == null ?
			await DbSet.AnyAsync(cancellationToken) :
			await DbSet.AnyAsync(predicate, cancellationToken);
	}

	public async Task<long> CountAsync(
		Expression<Func<TEntity, bool>> predicate = null,
		CancellationToken cancellationToken = default)
	{
		return predicate == null ?
			await DbSet.LongCountAsync(cancellationToken) :
			await DbSet.LongCountAsync(predicate, cancellationToken);
	}

	public async Task<TEntity> FirstOrDefaultAsync(
		Expression<Func<TEntity, bool>> predicate,
		CancellationToken cancellationToken = default)
	{
		if (predicate == null)
		{
			throw new ArgumentNullException(nameof(predicate));
		}

		return await DbSet.AsNoTracking()
			.FirstOrDefaultAsync(predicate, cancellationToken);
	}

	public async Task<TEntity> LastOrDefaultAsync(
		Expression<Func<TEntity, bool>> predicate,
		Expression<Func<TEntity, object>> keySelector,
		CancellationToken cancellationToken = default)
	{
		if (predicate == null)
		{
			throw new ArgumentNullException(nameof(predicate));
		}

		if (keySelector == null)
		{
			throw new ArgumentNullException(nameof(keySelector));
		}

		return await DbSet.AsNoTracking()
			.OrderBy(keySelector)
			.LastOrDefaultAsync(predicate, cancellationToken);
	}

	public async Task<IEnumerable<TEntity>> SearchAsync(
		Expression<Func<TEntity, bool>> predicate,
		CancellationToken cancellationToken = default)
	{
		if (predicate == null)
		{
			throw new ArgumentNullException(nameof(predicate));
		}

		return await DbSet.Where(predicate)
			.AsNoTracking()
			.ToListAsync(cancellationToken);
	}

	public async Task<TEntity> SingleOrDefaultAsync(
		Expression<Func<TEntity, bool>> predicate,
		CancellationToken cancellationToken = default)
	{
		if (predicate == null)
		{
			throw new ArgumentNullException(nameof(predicate));
		}

		return await DbSet.AsNoTracking()
			.SingleOrDefaultAsync(predicate, cancellationToken);
	}

	public async Task<int> SaveChangesAsync(
		bool acceptAllChangesOnSuccess = true,
		CancellationToken cancellationToken = default)
	{
		return await DbContext.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
	}
}