using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public class RegisterService : IRegisterService, IDisposable
    {
        private DealerSocket _client = null;
        public RegisterService()
        {
            Start();
        }

        private void Start()
        {
            _client = new DealerSocket();
            _client.Options.Identity = Encoding.Unicode.GetBytes(Guid.NewGuid().ToString());
            _client.Connect("tcp://127.0.0.1:5556");
            _client.ReceiveReady += Client_ReceiveReady;
        }

        void Client_ReceiveReady(object sender, NetMQSocketEventArgs e)
        {
            NetMQMessage msg = e.Socket.ReceiveMultipartMessage();
            var isEmpty = msg.IsEmpty;
            Console.WriteLine("REPLY {0}", msg.ToString());
            while (!isEmpty)
            {
                var result = e.Socket.ReceiveMultipartMessage();
                Console.WriteLine("REPLY {0}", result.ToString());
            }
        }

        public void SendMessage(Policy policy)
        {
            var payload = JsonConvert.SerializeObject(policy);
            var register = new Register(policy.Type, payload);
            var messageToServer = new NetMQMessage();
            messageToServer.AppendEmptyFrame();
            messageToServer.Append(JsonConvert.SerializeObject(register));
            _client.SendMultipartMessage(messageToServer);
        }

        public void Dispose()
        {
            _client.Dispose();
            _client = null;
        }
    }
}
