using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Config
{
    internal class WardConfig
    {
        public required string WardIdentifier { get; set; }
        public required string WardType { get; set; }
    }
}
