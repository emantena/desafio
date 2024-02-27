using RabbitMQ.Client;

namespace TodoWorker.Service.Interface;

public interface IRabbitMqService : IQueuService
{
	ConnectionFactory ConnectionFactory { get; }
}