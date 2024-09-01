using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NfzMock.Config
{
    internal class BusConfig
    {
        public required string Uri { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
    }
}
