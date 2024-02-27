using Dapper;
using DesafioB3.Domain.Dto;
using DesafioB3.Domain.Entity;
using DesafioB3.Repository.Interfaces;
using DesafioB3.Repository.Interfaces.Base;

namespace DesafioB3.Repository.Repositories;

public class TodoItemRepository : ITodoItemRepository
{
	private readonly IGenericRepository<Base.AppContext, TodoItem> _repository;

	public TodoItemRepository(IGenericRepository<Base.AppContext, TodoItem> repository)
	{
		_repository = repository;
	}

	public TodoItem Create(TodoItem item)
	{
		return _repository.Add(item);
	}

	public void Update(TodoItem item)
	{
		_repository.Update(item);
	}

	public async Task<IEnumerable<Column>> GetBoardAsync()
	{
		using var connection = _repository.GetDbConnection();
		var query = @"
                SELECT s.StatusId
                     , s.Name
					 , s.OrderExibition
					 , s.Active
                     , ti.TodoItemId AS CardId
                     , ti.Name
                     , ti.[Description]
                     , ti.CreateAt
                FROM TodoItem ti
                RIGHT JOIN [Status] s ON ti.StatusId = s.StatusId
				WHERE s.Active = 1";

		var statusDictionary = new Dictionary<int, Column>();

		_ = await connection.QueryAsync<Column, Card, Column>(query,
			(column, card) =>
			{
				if (!statusDictionary.TryGetValue(column.StatusId, out var statusEntry))
				{
					statusEntry = column;
					statusEntry.Card = new List<Card>();
					statusDictionary.Add(statusEntry.StatusId, statusEntry);
				}

				if (card?.CardId > 0)
				{
					statusEntry.Card.Add(card);
				}
				return statusEntry;
			},
			splitOn: "CardId"
		);

		return statusDictionary.Values;
	}


	public TodoItem GetById(int todoItemId)
	{
		return _repository.FirstOrDefault(x => x.TodoItemId == todoItemId);
	}

	public Task<IEnumerable<TodoItem>> ListAsync()
	{
		return _repository.SearchAsync(x => x.CreateAt < DateTime.Now);
	}
}