using DesafioB3.Service.ViewModels.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DesafioB3.API.Controllers.Base;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
	protected readonly ILogger _logger;

	public BaseController(ILogger logger)
	{
		_logger = logger;
	}
	protected new IActionResult Response(BaseResponse result, HttpStatusCode statusCode, string uri = "")
	{
		if (result == null)
		{
			base.Response.StatusCode = (int)HttpStatusCode.BadRequest;
			return BadRequest(new
			{
				success = false,
				errors = "Ocorreu um erro na aplicação"
			});
		}

		if (result.IsValid)
		{
			return statusCode switch
			{
				HttpStatusCode.OK => Ok(new { data = result.Value }),
				HttpStatusCode.BadRequest => BadRequest(new { success = false, errors = result.ToString() }),
				HttpStatusCode.Created => Created(uri, result.Value),
				HttpStatusCode.Accepted => Accepted(result.Value),
				HttpStatusCode.Forbidden => new ForbidResult(),
				HttpStatusCode.NoContent => NoContent(),
				HttpStatusCode.NotFound => NotFound(),
				HttpStatusCode.Unauthorized => Unauthorized(new { data = result.Value }),
				_ => Ok(result.Value),
			};
		}

		return BadRequest(new
		{
			succes = false,
			errors = result.Messages
		});
	}
}