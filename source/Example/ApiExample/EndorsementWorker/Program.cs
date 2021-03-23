using NetMQ;
using NetMQ.Sockets;
using System;

namespace EndorsementWorker
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var subSocket = new SubscriberSocket())
            {
                subSocket.Options.ReceiveHighWatermark = 1000;
                subSocket.Connect("tcp://localhost:12345");
                subSocket.Subscribe("endorsement");
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
