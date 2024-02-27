namespace TodoWorker.Service.Interface;


public interface IWorkerService
{
	//Task ProcessMessageAsync(string message);
	//Task StartListeningAsync();

	void ProcessMessages();
}
