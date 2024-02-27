using Flunt.Notifications;
using System.Diagnostics.CodeAnalysis;

namespace DesafioB3.Service.ViewModels.Response;

[ExcludeFromCodeCoverage]
public class BaseResponse
{
	private IList<Notification> messages { get; } = new List<Notification>();
	public IReadOnlyCollection<Notification> Messages => (IReadOnlyCollection<Notification>)messages;
	public bool IsValid => !messages.Any();
	public object Value { get; private set; }

	public BaseResponse()
	{
	}

	public BaseResponse(object @object) : this()
	{
		AddValue(@object);
	}

	public void AddValue(object @object)
	{
		Value = @object;
	}

	public void AddNotification(Notification notification)
	{
		messages.Add(notification);
	}

	public void AddNotifications(IEnumerable<Notification> notifications)
	{
		foreach (var notification in notifications)
		{
			AddNotification(notification);
		}
	}

	public override string ToString()
	{
		return string.Join(" - ", Messages.Select(x => x.Message));
	}
}