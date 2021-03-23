using NetMQ;
using NetMQ.Sockets;
using System;

namespace PolicyWorker
{
    class Program
    {
        private static readonly string PUBLISHER_ADDRESS = "tcp://localhost:12345";
        private static readonly int MAXIMUM_POOL_SIZE = 1000;
        private static readonly string TOPIC = "policy";

        static void Main(string[] args)
        {
            using (var subSocket = new SubscriberSocket())
            {
                subSocket.Options.ReceiveHighWatermark = MAXIMUM_POOL_SIZE;
                subSocket.Connect(PUBLISHER_ADDRESS);
                subSocket.Subscribe(TOPIC);

                Console.WriteLine("Conected to topic 'policy'...");
                while (true)
                {
                    string messageTopicReceived = subSocket.ReceiveFrameString();
                    string messageReceived = subSocket.ReceiveFrameString();
                    Console.WriteLine(messageReceived);

                    //Faz algo com a mensagem
                }
            }
        }
    }
}
