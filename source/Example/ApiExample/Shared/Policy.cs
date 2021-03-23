using System;
using System.Collections.Generic;

namespace Shared
{
    public class Policy
    {
        public string PolicyNumber { get; set; }
        public string Type { get; set; }
        public List<Instalment> Instalments { get; set; }
    }
}
