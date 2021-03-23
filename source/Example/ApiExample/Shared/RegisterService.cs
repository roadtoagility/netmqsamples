using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public class RegisterService : MessageService<Policy>
    {
        public RegisterService()
            :base("tcp://127.0.0.1:5556")
        {
            
        }

        public override void Client_ReceiveReady(object sender, NetMQSocketEventArgs e)
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

        protected override void SendMessage(Policy policy)
        {
            var payload = JsonConvert.SerializeObject(policy);
            var register = new Register(policy.Type, payload);
            var message = JsonConvert.SerializeObject(register).ToMessage();

            Client.SendMultipartMessage(message);
        }
    }
}
