using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DesafioB3.Repository.Interfaces.Base;

public interface IGenericRepository
{
	IDbConnection GetDbConnection();
	IDbTransaction GetDbTransaction();
}

public interface IGenericRepository<TDbContext, TEntity> : IGenericRepository, IDisposable,
	ISyncGenericRepository<TDbContext, TEntity>,
	IAsyncGenericRepository<TDbContext, TEntity>
	where TEntity : class
	where TDbContext : DbContext
{
	DbContext DbContext { get; }
}