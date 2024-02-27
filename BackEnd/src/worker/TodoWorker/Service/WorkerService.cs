using DesafioB3.Domain.Entity;
using DesafioB3.Repository.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using TodoWorker.Service.Interface;

namespace TodoWorker.Service;

public class WorkerService : IWorkerService
{
	private readonly ILogger<WorkerService> _logger;
	private readonly IConnection _connection;
	private readonly string queueName = "q-todoItem";
	private readonly string dlqName = "dlq-todoItem";
	private readonly ITodoItemRepository _repository;

	private IModel _channel;

	public WorkerService(ILogger<WorkerService> logger, ITodoItemRepository repository)
	{
		_logger = logger;
		_repository = repository;

		ConfigureRabbitMQ();
	}

	public void ProcessMessages()
	{
		var consumer = new EventingBasicConsumer(_channel);

		consumer.Received += (model, ea) =>
		{
			var body = ea.Body.ToArray();
			var message = Encoding.UTF8.GetString(body);

			try
			{
				if (TryProcessMessage(message))
				{
					_channel.BasicAck(ea.DeliveryTag, multiple: false);
					Console.WriteLine("Mensagem processada");
				}
				else
				{
					HandleFailedProcessing(ea, message);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Erro ao processar a mensagem: {ex.Message}");
				HandleFailedProcessing(ea, message);
			}
		};

		_channel.BasicConsume(queueName, autoAck: false, consumer);

		Console.WriteLine("Aguardando mensagens. Pressione Enter para sair.");
		Console.ReadLine();

		_channel.Close();
		_connection?.Close();
	}


	private void ConfigureRabbitMQ()
	{
		var connectionFactory = new ConnectionFactory()
		{
			UserName = "xbujgbmr",
			VirtualHost = "xbujgbmr",
			Password = "9735SgGxRAMvvYrmtKa2PznmpkJckMzS",
			HostName = "jackal.rmq.cloudamqp.com"
		};

		var connection = connectionFactory.CreateConnection();

		_channel = connection.CreateModel();
		_channel.ExchangeDeclare("dlq-exchange", ExchangeType.Fanout);
		_channel.QueueDeclare(
			dlqName,
			durable: true,
			exclusive: false,
			autoDelete: false,
			arguments: null
		);

		_channel.QueueBind(dlqName, "dlq-exchange", "");

		var arguments = new Dictionary<string, object>()
		{
			{"x-dead-letter-exchange", "dlq-exchange"}
		};

		_channel.QueueDeclare(queueName,
			durable: true,
			exclusive: false,
			autoDelete: false,
			arguments: arguments
		);
	}

	private bool TryProcessMessage(string message)
	{
		Console.WriteLine($"Processando a mensagem: {message}");

		var todoItemMessage = JsonConvert.DeserializeObject<TodoItem>(message);

		if (todoItemMessage is null)
		{
			return false;
		}

		if (todoItemMessage.TodoItemId == 0)
		{
			_ = _repository.Create(todoItemMessage);
		}
		else
		{
			var todoItem = _repository.GetById(todoItemMessage.TodoItemId);

			todoItem.Description = todoItemMessage.Description;
			todoItem.Name = todoItemMessage.Name;
			todoItem.StatusId = todoItemMessage.StatusId;

			_repository.Update(todoItem);
		}

		return true;
	}

	private void HandleFailedProcessing(BasicDeliverEventArgs ea, string message)
	{
		var retryCount = 0;
		if (ea.BasicProperties.Headers != null)
		{
			Console.WriteLine($"Reenviando mensagem: {message}");
			retryCount = ea.BasicProperties.Headers.TryGetValue("retry-count", out var value) ? (int)value : 0;
		}

		if (retryCount < 2)
		{
			Console.WriteLine($"Tentativa {retryCount + 1} de retentativa para a mensagem: {message}");
			PublishToRetryQueue(message, retryCount + 1);
			_channel.BasicAck(ea.DeliveryTag, multiple: false);
		}
		else
		{
			Console.WriteLine($"Excedeu o número máximo de retentativas. Movendo para a DLQ: {message}");
			MoveToDeadLetterQueue(ea);
		}
	}

	private void PublishToRetryQueue(string message, int retryCount)
	{
		var properties = _channel.CreateBasicProperties();
		properties.Headers = new Dictionary<string, object> { { "retry-count", retryCount } };

		_channel.BasicPublish(
			exchange: "",
			routingKey: queueName,
			basicProperties: properties,
			body: Encoding.UTF8.GetBytes(message)
		);
	}

	private void MoveToDeadLetterQueue(BasicDeliverEventArgs ea)
	{
		_channel.BasicNack(ea.DeliveryTag, multiple: false, requeue: false);
	}
}