using DesafioB3.API.Controllers.Base;
using DesafioB3.Service.Interfaces;
using DesafioB3.Service.ViewModels.Request;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DesafioB3.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TodoItemController : BaseController
	{
		private readonly ITodoItemService _todoItemService;

		public TodoItemController(ILogger<TodoItemController> logger, ITodoItemService todoItemService)
			: base(logger)
		{
			_todoItemService = todoItemService;
		}

		[HttpPost]
		public IActionResult Create(CreateTodoItemRequest request)
		{
			var response = _todoItemService.Create(request);
			return Response(response, HttpStatusCode.Created);
		}

		[HttpGet, Route("board")]
		public async Task<IActionResult> ListAsync()
		{
			var response = await _todoItemService.GetItemsAsync();
			return Response(response, HttpStatusCode.OK);
		}

		[HttpPatch]
		public IActionResult Update(UpdateTodoItemRequest request)
		{
			var response = _todoItemService.UpdateItem(request);
			return Response(response, HttpStatusCode.NoContent);
		}
	}
}
