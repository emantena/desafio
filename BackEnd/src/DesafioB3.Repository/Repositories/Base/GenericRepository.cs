using DesafioB3.Repository.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace DesafioB3.Repository.Base;

public sealed partial class GenericRepository<TDbContext, TEntity> : IGenericRepository<TDbContext, TEntity>
	where TEntity : class
	where TDbContext : DbContext
{
	private IDbConnection _dbConnection;
	public DbContext DbContext { get; private set; }
	public DbSet<TEntity> DbSet { get; private set; }

	public GenericRepository(TDbContext dbContext)
	{
		DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		DbSet = dbContext.Set<TEntity>();
	}

	public IDbConnection GetDbConnection()
	{
		if (_dbConnection != null)
		{
			return _dbConnection;
		}

		_dbConnection = DbContext.Database.GetDbConnection();

		return _dbConnection;
	}

	public IDbTransaction GetDbTransaction()
	{
		return DbContext.Database.CurrentTransaction?.GetDbTransaction();
	}

	private void SetAutoDetectChanges(bool enabled)
	{
		if (DbContext.ChangeTracker.AutoDetectChangesEnabled == enabled)
		{
			return;
		}

		DbContext.ChangeTracker.AutoDetectChangesEnabled = enabled;
	}

	#region Disposable Members

	private bool _disposed;

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	private void Dispose(bool disposing)
	{
		if (_disposed || !disposing)
		{
			return;
		}

		DbContext?.Dispose();
		DbContext = null;

		_disposed = true;
	}

	#endregion
}
