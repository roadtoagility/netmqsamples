using System;
using System.Collections.Generic;
using System.IO;
using ZeroMqHelloWorld.pub_sub;
using ZeroMqHelloWorld.request_reply;
using ZeroMqHelloWorld.router_dealer;

namespace ZeroMqHelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintPatterns();
            var action = Console.ReadLine();
            Run(action);
        }

        private static void Run(string action)
        {
            switch (action)
            {
                case "1":
                    RequestReplyStartup.Run();
                    break;
                case "2":
                    PubSubStartup.Run();
                    break;
                case "3":
                    RouterDealerStartup.Run();
                    break;
            }
        }

        private static void PrintPatterns()
        {
            Console.WriteLine("Choose pattern");
            Console.WriteLine("1: Request - Reply");
            Console.WriteLine("2: Publish - Subscriber");
            Console.WriteLine("3: Router - Dealer");
        }
    }
}
