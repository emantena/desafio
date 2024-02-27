using DesafioB3.IoC.Helpers;
using Newtonsoft.Json;

namespace DesafioB3.API.Midleware;

public class ExceptionMiddleware
{
	private readonly ILogger<ExceptionMiddleware> _logger;
	private readonly RequestDelegate _next;

	public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
	{
		_logger = logger;
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "An exception occurred");

			var response = context.Response;
			response.StatusCode = 500;
			response.ContentType = "application/json";

			var env = EnvironmentHelper.GetEnvironment();

			object error = env == "Development"
				? (new
				{
					message = "Ocorrreu um erro, tente novamente mais tarde",
					error = ex.Message,
					trace = ex.StackTrace
				})
				: (new
				{
					message = "Ocorrreu um erro, tente novamente mais tarde"
				});


			await response.WriteAsync(JsonConvert.SerializeObject(error));
		}
	}
}