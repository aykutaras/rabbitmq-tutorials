using System;
using System.Configuration;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMQSamples.HelloWorld.Consumer
{
    public class Receiver
    {
        public static void Run()
        {
            var factory = new ConnectionFactory
            {
                HostName = ConfigurationManager.AppSettings["RabbitMQUrl"]
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("hello", false, false, false, null);

                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume("hello", true, consumer);

                    Console.WriteLine(" [*] Waiting for messages. To exit press CTRL+C");

                    while (true)
                    {
                        var ea = consumer.Queue.Dequeue();

                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);
                    }
                }
            }
        }
    }
}
