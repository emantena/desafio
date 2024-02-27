using DesafioB3.Domain.Entity;

namespace DesafioB3.Repository.Interfaces;

public interface IUserRepository
{
	Task<User> CreateUserAsync(User user);
	Task<User> GetUserByEmailAsync(string email);
	Task<User> GetUserByIdAsync(int userId);
}