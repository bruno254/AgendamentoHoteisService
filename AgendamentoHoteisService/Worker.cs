using AgendamentoHoteis.Business.Interfaces;
using AgendamentoHoteis.Business.Models;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace AgendamentoHoteisService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        protected readonly ConnectionFactory factory;

        public Worker(ILogger<Worker> logger,
                      IServiceProvider serviceProvider,
                      IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
            _serviceProvider = serviceProvider;
            factory = new ConnectionFactory()
            {
                HostName = _configuration.GetSection("ConfigRabbitMQ").GetSection("HostName").Value,
                UserName = _configuration.GetSection("ConfigRabbitMQ").GetSection("UserName").Value,
                Password = _configuration.GetSection("ConfigRabbitMQ").GetSection("Password").Value
            };
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _logger.LogInformation("Aguardando agendamentos...");

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "agendamento",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += async (model, ea) =>
                {
                    _logger.LogInformation("Novo Agendamento", DateTimeOffset.Now);
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    Agendamento agendamento = JsonSerializer.Deserialize<Agendamento>(message);

                    using (IServiceScope scope = _serviceProvider.CreateScope())
                    {
                        var _service = scope.ServiceProvider.GetRequiredService<IAgendamentoService>();
                        await _service.AdicionarAgendamento(agendamento);
                    }
                };

                channel.BasicConsume(queue: "agendamento",
                                     autoAck: true,
                                     consumer: consumer);


                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation(
                        $"Worker ativo em: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    await Task.Delay(10000, stoppingToken);
                }
            }

            
        }
    }
}