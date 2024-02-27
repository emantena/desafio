using DesafioB3.API.Midleware;
using DesafioB3.IoC;

namespace DesafioB3.API;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.

		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

		builder.Services.Setup();

		builder.Services.AddCors(options =>
		{
			options.AddPolicy(name: MyAllowSpecificOrigins,
							  policy =>
							  {
								  policy.WithOrigins("http://localhost:4200")
									.AllowAnyHeader()
									.AllowAnyMethod();
							  });
		});

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();
		app.UseAuthentication();

		app.UseCors(MyAllowSpecificOrigins);

		app.UseAuthorization();

		app.MapControllers();
		app.UseMiddleware<ExceptionMiddleware>();
		app.UseMiddleware<RequestMiddleware>();

		app.Run();
	}
}