using DesafioB3.Domain.Entity;
using DesafioB3.Domain.ValueObjects;
using DesafioB3.IoC.Extensions;
using DesafioB3.Repository.Base;
using DesafioB3.Repository.Interfaces;
using DesafioB3.Repository.Interfaces.Base;
using DesafioB3.Repository.Repositories;
using DesafioB3.Service;
using DesafioB3.Service.Interfaces;
using DesafioB3.Service.Interfaces.Queue;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Diagnostics.CodeAnalysis;

namespace DesafioB3.IoC;

[ExcludeFromCodeCoverage]
public static class Startup
{
	private static IConfiguration Configuration;

	public static void Setup(this IServiceCollection services)
	{
		if (services == null)
		{
			throw new ArgumentNullException(nameof(services));
		}

		Configuration = services.AddConfiguration();

		services.AddSingleton(Configuration);
		services.AddDbContext();

		AddServices(services);
		AddSecurity(services);
	}

	private static void AddDbContext(this IServiceCollection services)
	{
		services.AddScoped<Repository.Repositories.Base.AppContext>();
		services.AddDbContext<Repository.Repositories.Base.AppContext>((serviceProvider, builder) =>
		{
			var connectionString = Configuration.GetSection("ConnectionStrings:AppConnection").Value;
			builder.UseSqlServer(connectionString);
		});

		services.AddGenericRepositories();
		services.AddRepository();
		services.AddThirdPartyServices();
	}

	public static void AddGenericRepositories(this IServiceCollection services)
	{
		services.AddScoped<IGenericRepository<Repository.Repositories.Base.AppContext, User>, GenericRepository<Repository.Repositories.Base.AppContext, User>>();
		services.AddScoped<IGenericRepository<Repository.Repositories.Base.AppContext, Status>, GenericRepository<Repository.Repositories.Base.AppContext, Status>>();
		services.AddScoped<IGenericRepository<Repository.Repositories.Base.AppContext, TodoItem>, GenericRepository<Repository.Repositories.Base.AppContext, TodoItem>>();

	}

	private static void AddRepository(this IServiceCollection services)
	{
		services.AddScoped<IUserRepository, UserRepository>();
		services.AddScoped<IStatusRepository, StatusRepository>();
		services.AddScoped<ITodoItemRepository, TodoItemRepository>();
	}

	private static void AddServices(IServiceCollection services)
	{
		services.AddScoped<IAuthService, AuthService>();
		services.AddScoped<IUserService, UserService>();
		services.AddScoped<IStatusService, StatusService>();
		services.AddScoped<ITodoItemService, TodoItemService>();

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

	private static void AddSecurity(IServiceCollection services)
	{
		var tokenConfigurations = new TokenConfigurations();
		new ConfigureFromConfigurationOptions<TokenConfigurations>(
			Configuration
			.GetSection("JwtConfiguration"))
			.Configure(tokenConfigurations);

		services.AddJwtSecurity(tokenConfigurations);
	}
}