using DesafioB3.Domain.Entity;
using DesafioB3.Repository.Base;
using DesafioB3.Repository.Interfaces;
using DesafioB3.Repository.Interfaces.Base;
using DesafioB3.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using TodoWorker.Extensions;
using TodoWorker.Service;
using TodoWorker.Service.Interface;

namespace TodoWorker;

public static class Startup
{
	private static IConfiguration Configuration;

	public static IServiceProvider Setup()
	{
		var services = new ServiceCollection();

		Configuration = services.AddConfiguration();

		services.AddLogging(builder => builder.AddConsole());

		services.AddSingleton(Configuration);
		services.AddDbContext();

		AddServices(services);

		return services.BuildServiceProvider();
	}

	private static void AddDbContext(this IServiceCollection services)
	{
		services.AddScoped<DesafioB3.Repository.Repositories.Base.AppContext>();
		services.AddDbContext<DesafioB3.Repository.Repositories.Base.AppContext>((serviceProvider, builder) =>
		{
			var connectionString = Configuration.GetSection("ConnectionStrings:AppConnection").Value;
			builder.UseSqlServer(connectionString);
		});

		services.AddGenericRepositories();
		services.AddRepository();
	}

	public static void AddGenericRepositories(this IServiceCollection services)
	{
		services.AddScoped<IGenericRepository<DesafioB3.Repository.Repositories.Base.AppContext, TodoItem>, GenericRepository<DesafioB3.Repository.Repositories.Base.AppContext, TodoItem>>();

	}

	private static void AddRepository(this IServiceCollection services)
	{
		services.AddScoped<ITodoItemRepository, TodoItemRepository>();
	}

	private static void AddServices(IServiceCollection services)
	{
		services.AddScoped<IWorkerService, WorkerService>();

		services.AddThirdPartyServices();
	}

	private static void AddThirdPartyServices(this IServiceCollection services)
	{
		services.AddScoped<IRabbitMqService, RabbitMqService>(p =>
		{
			var connectionFactory = new ConnectionFactory();

			new ConfigureFromConfigurationOptions<ConnectionFactory>(
				Configuration
				.GetSection("RabbitMqConfiguration"))
				.Configure(connectionFactory);

			return new RabbitMqService(connectionFactory);
		});
	}
}