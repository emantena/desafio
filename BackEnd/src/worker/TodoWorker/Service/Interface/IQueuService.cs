namespace TodoWorker.Service.Interface;

public interface IQueuService
{
	void SendMessage(object message, string queueName);
}