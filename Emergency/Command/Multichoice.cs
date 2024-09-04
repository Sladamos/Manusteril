using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Command
{
    internal class Multichoice<T>
    {
        public required string Name { get; set; }

        public required string DefaultDescription { get; set; }

        public required Func<T> ParameterSupplier { get; set; }

        public Func<T, string>? ParameterTransformator { get; set; }

        public Func<T, bool>? ParameterValidator { get; set; }

        public required List<T> Values { get; set; }
    }
}
