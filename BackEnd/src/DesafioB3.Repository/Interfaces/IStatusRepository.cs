using DesafioB3.Domain.Entity;

namespace DesafioB3.Repository.Interfaces;

public interface IStatusRepository
{
	Task<IEnumerable<Status>> GetAllStatusAsync();
}
