using NetMQ;
using NetMQ.Sockets;
using Shared;
using Shared.Extensions;
using System;

namespace Broker
{
    class Program
    {
        private static readonly string SERVER_ADDRESS = "@tcp://127.0.0.1:5556";
        private static readonly string PUBLISHER_ADDRESS = "tcp://*:12345";
        private static readonly int MAXIMUM_POOL_SIZE = 1000;

        static void Main(string[] args)
        {
            using (var server = new RouterSocket(SERVER_ADDRESS))
            using (var pubSocket = new PublisherSocket())
            {
                pubSocket.Options.SendHighWatermark = MAXIMUM_POOL_SIZE;
                pubSocket.Bind(PUBLISHER_ADDRESS);

                while (true)
                {
                    var clientMessage = server.ReceiveMultipartMessage();
                    PrintFrames("Server receiving", clientMessage);
                    if (clientMessage.FrameCount == 3)
                    {
                        var clientAddress = clientMessage[0];
                        var clientOriginalMessage = clientMessage[2].ConvertToString();
                        string response = string.Format("{0} back from server {1}", clientOriginalMessage, DateTime.Now.ToLongTimeString());

                        server.SendMultipartMessage(response.ToMessage(clientAddress));

                        var register = Newtonsoft.Json.JsonConvert.DeserializeObject<Register>(clientOriginalMessage);
                        pubSocket.SendMoreFrame(register.Type).SendFrame(register.Payload);
                    }
                }
            }
        }

        static void PrintFrames(string operationType, NetMQMessage message)
        {
            for (int i = 0; i < message.FrameCount; i++)
            {
                Console.WriteLine("{0} Socket : Frame[{1}] = {2}", operationType, i,
                    message[i].ConvertToString());
            }
        }
    }
}
