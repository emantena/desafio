using DesafioB3.Domain.Entity;
using DesafioB3.Domain.Enums;
using DesafioB3.Domain.ValueObjects;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DesafioB3.Service.Token;

public class JwtService
{
	private readonly TokenConfigurations _tokenConfiguration;
	private readonly SigningConfigurations _signingConfigurations;

	private readonly DateTime CreationDate = DateTime.Now;

	public JwtService(TokenConfigurations jwtConfiguration,
		SigningConfigurations signingConfigurations)
	{
		_tokenConfiguration = jwtConfiguration;
		_signingConfigurations = signingConfigurations;
	}

	public Jwt CreateToken(User user)
	{
		var identity = GetClaims(user);

		var expirationDate = GetDateToExpires();

		var handler = new JwtSecurityTokenHandler();

		var securityToken = GetSecurityToken(identity, expirationDate, handler);

		var token = handler.WriteToken(securityToken);
		var refreshToken = Guid.NewGuid().ToString().Replace("-", string.Empty);

		var result = new Jwt(
			CreationDate.ToString("yyyy-MM-dd HH:mm:ss"),
			expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
			token,
			refreshToken,
			"OK");

		return result;
	}

	private static ClaimsIdentity GetClaims(User user)
	{
		return new ClaimsIdentity(
			new[] {
					new Claim(JwtRegisteredClaimNames.Email, user.Email),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
					new Claim(JwtRegisteredClaimNames.NameId, user.Name),
					new Claim(ClaimTypes.Role, ((Role)Enum.ToObject(typeof(Role), user.RoleId)).ToString()),
					new Claim("userId", user.UserId.ToString()),
			});
	}

	private DateTime GetDateToExpires()
	{
		return CreationDate.AddDays(_tokenConfiguration.TotalHoursExpiresToken);
	}

	private SecurityToken GetSecurityToken(ClaimsIdentity identity,
		DateTime expirationDate,
		JwtSecurityTokenHandler handler
	)
	{
		return handler.CreateToken(
			new SecurityTokenDescriptor
			{
				Issuer = _tokenConfiguration.Issuer,
				Audience = _tokenConfiguration.Audience,
				SigningCredentials = _signingConfigurations.SigningCredentials,
				Subject = identity,
				NotBefore = CreationDate,
				Expires = expirationDate
			});
	}
}