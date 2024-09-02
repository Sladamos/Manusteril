using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Command
{
    internal class Condition
    {
        public required Func<bool> Predicate { get; set; }

        public required string Reason { get; set; }
    }
}
