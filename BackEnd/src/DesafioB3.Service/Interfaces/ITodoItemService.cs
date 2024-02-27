using DesafioB3.Service.ViewModels.Request;
using DesafioB3.Service.ViewModels.Response;

namespace DesafioB3.Service.Interfaces;

public interface ITodoItemService
{
	Task<BaseResponse> GetItemsAsync();
	BaseResponse Create(CreateTodoItemRequest request);
	BaseResponse UpdateItem(UpdateTodoItemRequest request);

}
