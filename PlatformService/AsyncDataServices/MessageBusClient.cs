using System;
using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;
using RabbitMQ.Client;

namespace PlatformService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly object _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                Console.WriteLine("--> Connecetd to MessageBus");
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"--> Could not connect to the Message Bus: {ex.Message}");
            }
        }
        public void PublishNewPlatform(PlatformPublishedDto platformPublishedDto)
        {
            throw new System.NotImplementedException();
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) 
        {
            Console.WriteLine("--> RabbitMQ Connection Shutdown");
        }
    }
}
