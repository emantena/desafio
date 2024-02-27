using DesafioB3.Repository.Repositories.Base.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace DesafioB3.Repository.Base;
public sealed partial class GenericRepository<TDbContext, TEntity>
{
	public TEntity Add(TEntity entity)
	{
		DbSet.Add(entity);

		SaveChanges();

		return entity;
	}

	public void AddRange(IEnumerable<TEntity> entities)
	{
		DbSet.AddRange(entities);

		SaveChanges();
	}

	public void Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties)
	{
		if (properties.IsNullOrEmpty())
		{
			DbSet.Update(entity);
			SaveChanges();

			return;
		}

		var originalAutoDetectChangesValue = DbContext.ChangeTracker.AutoDetectChangesEnabled;
		SetAutoDetectChanges(enabled: false);

		DbContext.Entry(entity).State = EntityState.Unchanged;

		var modifiedProperty = properties.Select(o => o.GetPropertyInfo());

		var entityProperties = DbContext.Entry(entity).Metadata.GetProperties();

		foreach (var property in entityProperties)
		{
			if (modifiedProperty.Contains(property.PropertyInfo))
			{
				DbContext.Entry(entity).Property(property.Name).IsModified = true;
			}
			else
			{
				DbContext.Entry(entity).Property(property.Name).IsModified = false;
			}
		}

		SetAutoDetectChanges(originalAutoDetectChangesValue);

		SaveChanges();
	}

	public void UpdateRange(IEnumerable<TEntity> entities)
	{
		if (entities.IsNullOrEmpty())
		{
			return;
		}

		DbSet.UpdateRange(entities);

		SaveChanges();
	}

	public void RemoveByEntity(TEntity entity)
	{
		DbSet.Remove(entity);

		SaveChanges();
	}

	public void RemoveSingle(Expression<Func<TEntity, bool>> predicate)
	{
		var entity = DbSet.AsTracking().SingleOrDefault(predicate);

		if (entity == null)
		{
			throw new InvalidOperationException($"Entity not found!");
		}

		DbSet.Remove(entity);

		SaveChanges();
	}

	public void RemoveRange(IEnumerable<TEntity> entities)
	{
		if (entities.IsNullOrEmpty())
		{
			return;
		}

		DbSet.RemoveRange(entities);

		SaveChanges();
	}

	public bool Any(Expression<Func<TEntity, bool>> predicate = null)
	{
		return predicate == null ?
			DbSet.Any() :
			DbSet.Any(predicate);
	}

	public long Count(Expression<Func<TEntity, bool>> predicate = null)
	{
		return predicate == null ?
			DbSet.LongCount() :
			DbSet.LongCount(predicate);
	}

	public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
	{
		if (predicate == null)
		{
			throw new ArgumentNullException(nameof(predicate));
		}

		return DbSet.AsNoTracking()
			.FirstOrDefault(predicate);
	}

	public TEntity LastOrDefault(
		Expression<Func<TEntity, bool>> predicate,
		Expression<Func<TEntity, object>> keySelector)
	{
		if (predicate == null)
		{
			throw new ArgumentNullException(nameof(predicate));
		}

		return DbSet.AsNoTracking()
			.OrderBy(keySelector)
			.LastOrDefault(predicate);
	}

	public IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate)
	{
		if (predicate == null)
		{
			throw new ArgumentNullException(nameof(predicate));
		}

		return DbSet.Where(predicate)
			 .AsNoTracking()
			 .ToList();
	}

	public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
	{
		if (predicate == null)
		{
			throw new ArgumentNullException(nameof(predicate));
		}

		return DbSet.AsNoTracking()
			.SingleOrDefault(predicate);
	}

	public int SaveChanges(bool acceptAllChangesOnSuccess = true)
	{
        return DbContext.SaveChanges(acceptAllChangesOnSuccess);
    }
}