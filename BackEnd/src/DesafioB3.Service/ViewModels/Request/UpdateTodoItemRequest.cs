namespace DesafioB3.Service.ViewModels.Request;
public class UpdateTodoItemRequest
{
	public int CardId { get; set; }
	public int StatusId { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
}
