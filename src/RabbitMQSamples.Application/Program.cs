namespace RabbitMQSamples.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            HelloWorldSample();
        }

        static void HelloWorldSample()
        {
            for (var i = 0; i < 10; i++)
            {
                HelloWorld.Producer.Sender.Run();
            }

            HelloWorld.Consumer.Receiver.Run();
        }
    }
}
