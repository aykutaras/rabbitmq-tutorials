using System;

namespace RabbitMQSamples.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                HelloWorldSample();
                return;
            }

            switch (args[0])
            {
                case "worker":
                    WorkQueuesConsumer();
                    break;
                case "newtask":
                    while (true)
                    {
                        Console.WriteLine("Give a message to send:");
                        var message = Console.ReadLine();
                        WorkQueuesProducer(message);
                    }
            }
        }

        static void HelloWorldSample()
        {
            for (var i = 0; i < 10; i++)
            {
                HelloWorld.Producer.Sender.Run();
            }

            HelloWorld.Consumer.Receiver.Run();
        }

        static void WorkQueuesProducer(string message)
        {
            WorkQueues.Producer.Sender.Run(message);
        }

        static void WorkQueuesConsumer()
        {
            WorkQueues.Consumer.Receiver.Run();
        }
    }
}
