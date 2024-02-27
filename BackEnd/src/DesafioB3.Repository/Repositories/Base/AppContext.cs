using Microsoft.EntityFrameworkCore;

namespace DesafioB3.Repository.Repositories.Base;

public class AppContext : DbContext
{
	public AppContext(DbContextOptions options, DbContextOptionsBuilder optionsBuilder)
		: base(options)
	{
		ChangeTracker.LazyLoadingEnabled = false;
		ChangeTracker.AutoDetectChangesEnabled = false;
		ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;


		optionsBuilder.EnableSensitiveDataLogging();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);
	}
}