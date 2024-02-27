using DesafioB3.API.Controllers.Base;
using DesafioB3.Service.Interfaces;
using DesafioB3.Service.ViewModels.Request;
using DesafioB3.Service.ViewModels.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DesafioB3.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseController
{
	private readonly IAuthService _authService;

	public AuthController(ILogger<AuthController> logger, IAuthService authService)
		: base(logger)
	{
		_authService = authService;
	}

	[HttpPost]
	public async Task<IActionResult> Auth(AuthRequest request)
	{
		var token = await _authService.AuthenticateAsync(request);

		if (token == null)
		{
			return Response(new BaseResponse("login ou senha inv�lido"), HttpStatusCode.Unauthorized);
		}

		return Ok(token);
	}
}