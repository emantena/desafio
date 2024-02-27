namespace DesafioB3.Domain.Entity;

public class TodoItem
{
	public int TodoItemId { get; set; }
	public int StatusId { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public DateTime CreateAt { get; set; }

	//public virtual Status Status { get; set; }

	public TodoItem()
	{

	}

	public TodoItem(int statusId, string name, string description)
	{
		StatusId = statusId;
		Name = name;
		Description = description;
		CreateAt = DateTime.Now;
	}
}
