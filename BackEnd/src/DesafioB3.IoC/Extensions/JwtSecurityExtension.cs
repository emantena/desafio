using DesafioB3.Domain.ValueObjects;
using DesafioB3.Service.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace DesafioB3.IoC.Extensions
{
	[ExcludeFromCodeCoverage]
	public static class JwtSecurityExtension
	{
		public static IServiceCollection AddJwtSecurity(this IServiceCollection services,
			TokenConfigurations tokenConfiguration)
		{
			services.AddSingleton(tokenConfiguration);

			services.AddScoped<JwtService>();

			var signingConfigurations = new SigningConfigurations(tokenConfiguration);
			services.AddSingleton(signingConfigurations);

			services.AddAuthentication(authOptions =>
			{
				authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(bearerOptions =>
			{
				var paramsValidation = bearerOptions.TokenValidationParameters;

				paramsValidation.IssuerSigningKey = signingConfigurations.Key;
				paramsValidation.ValidAudience = tokenConfiguration.Audience;
				paramsValidation.ValidIssuer = tokenConfiguration.Issuer;
				paramsValidation.ValidateIssuerSigningKey = true;
				paramsValidation.ValidateLifetime = true;
				paramsValidation.ClockSkew = TimeSpan.Zero;
			});

			services.AddAuthorization(auth =>
			{
				auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
					.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
					.RequireAuthenticatedUser().Build());
			});

			return services;
		}
	}
}
