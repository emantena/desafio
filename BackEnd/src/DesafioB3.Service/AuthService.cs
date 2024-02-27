using DesafioB3.Domain.Entity;
using DesafioB3.Domain.ValueObjects;
using DesafioB3.Repository.Interfaces;
using DesafioB3.Service.Interfaces;
using DesafioB3.Service.Token;
using DesafioB3.Service.ViewModels.Request;

namespace DesafioB3.Service;

public class AuthService : IAuthService
{
	private readonly IUserRepository _userRepository;
	private readonly JwtService _jwtService;

	public AuthService(IUserRepository applicationRepository, JwtService jwtService)
	{
		_userRepository = applicationRepository;
		_jwtService = jwtService;
	}

	public async Task<Jwt> AuthenticateAsync(AuthRequest request)
	{

		var user = await _userRepository.GetUserByEmailAsync(request.Email);

		if (!IsValidUser(user, request))
		{
			return null;
		}

		return _jwtService.CreateToken(user);
	}

	private static bool IsValidUser(User user, AuthRequest request)
	{
		if (user == null)
		{
			return false;
		}

		if (!user.Active)
		{
			return false;
		}

		return Password.IsMatch(request.Password, user.Password);
	}
}