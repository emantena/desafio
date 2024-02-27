using DesafioB3.Domain.ValueObjects;
using DesafioB3.Service.ViewModels.Request;

namespace DesafioB3.Service.Interfaces;

public interface IAuthService
{
	Task<Jwt> AuthenticateAsync(AuthRequest request);
}