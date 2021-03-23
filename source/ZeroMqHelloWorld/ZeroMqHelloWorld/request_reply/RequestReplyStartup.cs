using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeroMqHelloWorld.request_reply
{
    public static class RequestReplyStartup
    {
        public static void Run()
        {
            using (var responseSocket = new ResponseSocket("@tcp://*:5555"))
            using (var requestSocket = new RequestSocket(">tcp://localhost:5555"))
            {
                Console.WriteLine("requestSocket : Sending 'Hello'");
                requestSocket.SendFrame("Hello");
                var message = responseSocket.ReceiveFrameString();
                Console.WriteLine("responseSocket : Server Received '{0}'", message);
                Console.WriteLine("responseSocket Sending 'World'");
                responseSocket.SendFrame("World");
                message = requestSocket.ReceiveFrameString();
                Console.WriteLine("requestSocket : Received '{0}'", message);
                Console.ReadLine();
            }
        }
    }
}
