using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public class Register
    {
        public string Type { get; protected set; }
        public string Payload { get; protected set; }

        public Register(string type, string payload)
        {
            Type = type;
            Payload = payload;
        }
    }
}
