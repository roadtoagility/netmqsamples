using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZeroMqHelloWorld.pub_sub
{
    public static class PubSubStartup
    {
        public static void Run()
        {
            var server = Task.Factory.StartNew(() =>
            {
                Random rand = new Random(50);
                using (var pubSocket = new PublisherSocket())
                {
                    pubSocket.Options.SendHighWatermark = 1000;
                    pubSocket.Bind("tcp://*:12345");
                    for (var i = 0; i < 100; i++)
                    {
                        var randomizedTopic = rand.NextDouble();
                        if (randomizedTopic > 0.5)
                        {
                            var msg = "TopicA msg-" + i;
                            pubSocket.SendMoreFrame("TopicA").SendFrame(msg);
                        }
                        else
                        {
                            var msg = "TopicB msg-" + i;
                            pubSocket.SendMoreFrame("TopicB").SendFrame(msg);
                        }
                        Thread.Sleep(500);
                    }
                }
            });

            var client = Task.Factory.StartNew(() =>
            {
                
                
                var allowableCommandLineArgs = new[] { "TopicA", "TopicB", "All" };

                var args = string.Empty;

                while(args.Length == 0 || !allowableCommandLineArgs.Contains(args))
                {
                    Console.WriteLine("Expected one argument, either " +
                                      "'TopicA', 'TopicB' or 'All'");
                    args = Console.ReadLine();
                }

                string topic = args == "All" ? "" : args;

                Console.WriteLine("Subscriber started for Topic : {0}", topic);

                using (var subSocket = new SubscriberSocket())
                {
                    subSocket.Options.ReceiveHighWatermark = 1000;
                    subSocket.Connect("tcp://localhost:12345");
                    subSocket.Subscribe(topic);
                    Console.WriteLine("Subscriber socket connecting...");
                    while (true)
                    {
                        string messageTopicReceived = subSocket.ReceiveFrameString();
                        string messageReceived = subSocket.ReceiveFrameString();
                        Console.WriteLine($"Message from broker: {messageReceived}");
                    }
                }
            });

            Task.WaitAll(server, client);

        }
    }
}
