using DesafioB3.Domain.Entity;
using DesafioB3.Service.ViewModels.Response;

namespace DesafioB3.Service.Interfaces;

public interface IUserService
{
	Task<BaseResponse> GetUserProfileAsync(int userId);
	Task<BaseResponse> CreateUserAsync(User user);
}