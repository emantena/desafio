using DesafioB3.IoC.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace DesafioB3.IoC.Extensions
{
	[ExcludeFromCodeCoverage]
	public static class ConfigurationExtensions
	{
		public static IConfiguration AddConfiguration(this IServiceCollection services)
		{
			var environment = EnvironmentHelper.GetEnvironment();

			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true)
				.Build();

			services.AddSingleton<IConfiguration>(configuration);

			return configuration;
		}
	}
}
