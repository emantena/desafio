using DesafioB3.Domain.Entity;
using DesafioB3.Repository.Interfaces;
using DesafioB3.Repository.Interfaces.Base;

namespace DesafioB3.Repository.Repositories;

public class StatusRepository : IStatusRepository
{
	private readonly IGenericRepository<Base.AppContext, Status> _repository;

	public StatusRepository(IGenericRepository<Base.AppContext, Status> repository)
	{
		_repository = repository;
	}
	public async Task<IEnumerable<Status>> GetAllStatusAsync()
	{
		return await _repository.SearchAsync(x => x.Active);
	}
}