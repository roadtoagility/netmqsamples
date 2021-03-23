using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public abstract class MessageService<T> : IRegisterService<T>, IDisposable
    {
        public DealerSocket Client { get; private set; } = null;
        public string Endpoint { get; private set; }

        public MessageService(string endpoint)
        {
            Endpoint = endpoint;
        }

        public void Send(T entity)
        {
            if (Client == null || Client.IsDisposed)
                Start();

            SendMessage(entity);
        }
        private void Start()
        {
            Client = new DealerSocket();
            Client.Options.Identity = Encoding.Unicode.GetBytes(Guid.NewGuid().ToString());
            Client.Connect(Endpoint);
            Client.ReceiveReady += Client_ReceiveReady;
        }

        public abstract void Client_ReceiveReady(object sender, NetMQSocketEventArgs e);

        protected abstract void SendMessage(T message);

        public void Dispose()
        {
            Client.Dispose();
            Client = null;
        }
    }
}
