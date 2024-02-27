using Flunt.Notifications;

namespace DesafioB3.Domain.Entity;
public class User : Notifiable<Notification>
{
	public int UserId { get; set; }
	public int RoleId { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public bool Active { get; set; }
	public DateTime? CreateAt { get; set; }

	public User()
	{

	}

	public User(int roleId, string name, string email, string password)
	{
		RoleId = roleId;
		Name = name;
		Email = email;
		CreateAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
		Password = ValueObjects.Password.HashPassword(password);
		Active = true;
	}
}