using DesafioB3.Domain.Entity;
using DesafioB3.Repository.Interfaces;
using DesafioB3.Repository.Interfaces.Base;

namespace DesafioB3.Repository.Repositories;

public class UserRepository : IUserRepository
{

	private readonly IGenericRepository<Base.AppContext, User> _repository;

	public UserRepository(IGenericRepository<Base.AppContext, User> repository)
	{
		_repository = repository;
	}

	public async Task<User> CreateUserAsync(User user)
	{
		return await _repository.AddAsync(user);
	}

	public async Task<User> GetUserByEmailAsync(string email)
	{
		return await _repository.FirstOrDefaultAsync(x => x.Email == email);
	}

	public async Task<User> GetUserByIdAsync(int userId)
	{
		return await _repository.FirstOrDefaultAsync(x => x.UserId == userId && x.Active);
	}
}