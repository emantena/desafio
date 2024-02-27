namespace DesafioB3.Domain.Entity;

public class Status
{
	public int StatusId { get; set; }
	public string Name { get; set; }
	public int OrderExibition { get; set; }
	public bool Active { get; set; }

	public virtual List<TodoItem> TodoItems { get; set; }
}