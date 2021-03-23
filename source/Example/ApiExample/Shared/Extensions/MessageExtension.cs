using NetMQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Extensions
{
    public static class MessageExtension
    {
        public static NetMQMessage ToMessage(this string source, NetMQFrame address)
        {
            var message = new NetMQMessage();
            message.Append(address);
            message.AppendEmptyFrame();
            message.Append(source);

            return message;
        }

        public static NetMQMessage ToMessage(this string source)
        {
            var message = new NetMQMessage();
            message.AppendEmptyFrame();
            message.Append(source);
            return message;
        }
    }
}
