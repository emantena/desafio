namespace DesafioB3.Service.ViewModels.Response;

public class UserProfileResponse
{
	public int UserId { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
	public string Role { get; set; }

	public UserProfileResponse(int userId, string name, string email, string role)
	{
		UserId = userId;
		Name = name;
		Email = email;
		Role = role;
	}
}