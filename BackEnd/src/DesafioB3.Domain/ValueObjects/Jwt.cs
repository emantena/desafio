namespace DesafioB3.Domain.ValueObjects;
public class Jwt
{
	public string Created { get; }
	public string Expiration { get; }
	public string AccessToken { get; }
	public string RefreshToken { get; }
	public string Message { get; }

	public Jwt(string created, string expiration, string accessToken, string refreshToken, string message = null)
	{
		Created = created;
		Expiration = expiration;
		AccessToken = accessToken;
		RefreshToken = refreshToken;
		Message = message;
	}
}