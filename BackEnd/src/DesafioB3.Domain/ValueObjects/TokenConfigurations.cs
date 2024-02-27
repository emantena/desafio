namespace DesafioB3.Domain.ValueObjects;

public class TokenConfigurations
{
	public string AccessRole { get; set; }
	public string Secret { get; set; }
	public string Audience { get; set; }
	public string Issuer { get; set; }
	public int TotalHoursExpiresToken { get; set; }
	public int TotalHoursExpiresRefreshToken { get; set; }
}