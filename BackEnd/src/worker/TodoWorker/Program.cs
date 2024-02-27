using TodoWorker.Service.Interface;

namespace TodoWorker;

internal class Program
{
	static void Main(string[] args)
	{

		var serviceProvider = Startup.Setup();

		serviceProvider.GetService<IWorkerService>()
			?.ProcessMessages();

		Console.ReadLine();
	}
}