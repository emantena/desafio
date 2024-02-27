using RabbitMQ.Client;

namespace DesafioB3.Service.Interfaces.Queue;

public interface IRabbitMqService : IQueuService
{
	ConnectionFactory ConnectionFactory { get; }
}