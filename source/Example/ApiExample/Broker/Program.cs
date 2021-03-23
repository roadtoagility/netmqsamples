using NetMQ;
using NetMQ.Sockets;
using Shared;
using System;

namespace Broker
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var server = new RouterSocket("@tcp://127.0.0.1:5556"))
            using (var pubSocket = new PublisherSocket())
            {
                pubSocket.Options.SendHighWatermark = 1000;
                pubSocket.Bind("tcp://*:12345");
                var count = 0;

                while (true)
                {
                    var clientMessage = server.ReceiveMultipartMessage();
                    PrintFrames("Server receiving", clientMessage);
                    if (clientMessage.FrameCount == 3)
                    {
                        var clientAddress = clientMessage[0];
                        var clientOriginalMessage = clientMessage[2].ConvertToString();
                        string response = string.Format("{0} back from server {1}",
                            clientOriginalMessage, DateTime.Now.ToLongTimeString());
                        var messageToClient = new NetMQMessage();
                        messageToClient.Append(clientAddress);
                        messageToClient.AppendEmptyFrame();
                        messageToClient.Append(response);
                        server.SendMultipartMessage(messageToClient);

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
