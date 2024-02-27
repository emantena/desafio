using DesafioB3.Domain.ValueObjects;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DesafioB3.Service.Token;

public class SigningConfigurations
{
	public SecurityKey Key { get; }
	public SigningCredentials SigningCredentials { get; }

	public SigningConfigurations(TokenConfigurations tokenConfigurations)
	{
		Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret));

		SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);
	}
}