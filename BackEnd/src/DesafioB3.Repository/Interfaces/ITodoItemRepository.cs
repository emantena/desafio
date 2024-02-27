using DesafioB3.Domain.Dto;
using DesafioB3.Domain.Entity;

namespace DesafioB3.Repository.Interfaces;

public interface ITodoItemRepository
{
	Task<IEnumerable<Column>> GetBoardAsync();

	TodoItem Create(TodoItem item);
	void Update(TodoItem item);
	TodoItem GetById(int todoItemId);
}