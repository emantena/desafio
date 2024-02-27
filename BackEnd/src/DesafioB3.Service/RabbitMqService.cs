using DesafioB3.Service.Interfaces.Queue;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace DesafioB3.Service;

public class RabbitMqService : IRabbitMqService
{
	public ConnectionFactory ConnectionFactory { get; }

	public RabbitMqService(ConnectionFactory connectionFactory)
	{
		ConnectionFactory = connectionFactory;
	}

	public void SendMessage(object message, string queueName)
	{
		using var connection = ConnectionFactory.CreateConnection();
		using var channel = connection.CreateModel();
		var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

		channel.BasicPublish(exchange: string.Empty, routingKey: queueName, basicProperties: null, body: body);
	}
}