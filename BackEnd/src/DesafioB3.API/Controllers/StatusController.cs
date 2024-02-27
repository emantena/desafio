using DesafioB3.API.Controllers.Base;
using DesafioB3.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DesafioB3.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StatusController : BaseController
	{
		private readonly IStatusService _statusService;
		public StatusController(ILogger<StatusController> logger, IStatusService statusService) : base(logger)
		{
			_statusService = statusService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAsync()
		{
			var response = await _statusService.GetStatusAsync();
			return Response(response, HttpStatusCode.OK);
		}
	}
}
