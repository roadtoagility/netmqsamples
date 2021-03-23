using NetMQ;
using NetMQ.Sockets;
using System;

namespace EndorsementWorker
{
    class Program
    {
        private static readonly string PUBLISHER_ADDRESS = "tcp://localhost:12345";
        private static readonly int MAXIMUM_POOL_SIZE = 1000;
        private static readonly string TOPIC = "endorsement";

        static void Main(string[] args)
        {
            using (var subSocket = new SubscriberSocket())
            {
                subSocket.Options.ReceiveHighWatermark = MAXIMUM_POOL_SIZE;
                subSocket.Connect(PUBLISHER_ADDRESS);
                subSocket.Subscribe(TOPIC);

                Console.WriteLine("Conected to topic 'Endorsement'...");
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
