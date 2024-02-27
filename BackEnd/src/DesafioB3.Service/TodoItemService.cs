using DesafioB3.Domain.Entity;
using DesafioB3.Domain.Enums;
using DesafioB3.Repository.Interfaces;
using DesafioB3.Service.Interfaces;
using DesafioB3.Service.Interfaces.Queue;
using DesafioB3.Service.ViewModels.Request;
using DesafioB3.Service.ViewModels.Response;
using Flunt.Notifications;

namespace DesafioB3.Service;

public class TodoItemService : ITodoItemService
{
	private readonly ITodoItemRepository _repository;
	private readonly IQueuService queuService;

	public TodoItemService(ITodoItemRepository repository, IRabbitMqService rabbitMq)
	{
		_repository = repository;
		queuService = rabbitMq;
	}

	public BaseResponse Create(CreateTodoItemRequest request)
	{
		var response = new BaseResponse();

		var item = new TodoItem((int)ColumCard.Todo, request.Name, request.Description);

		queuService.SendMessage(item, "q-todoItem");

		return response;
	}

	public async Task<BaseResponse> GetItemsAsync()
	{
		var response = new BaseResponse();

		response.AddValue(await _repository.GetBoardAsync());

		return response;
	}

	public BaseResponse UpdateItem(UpdateTodoItemRequest request)
	{
		var response = new BaseResponse();

		var item = _repository.GetById(request.CardId);

		if (item is null)
		{
			response.AddNotification(new Notification("", "Item não encotrado"));
			return response;
		}

		item.Description = request.Description;
		item.StatusId = request.StatusId;
		item.Name = request.Name;

		queuService.SendMessage(item, "q-todoItem");

		return response;
	}
}