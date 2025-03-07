﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Command
{
    internal class Multichoice<T>
    {
        public required string Name { get; set; }

        public required string DefaultDescription { get; set; }

        public required Func<T> ParameterSupplier { get; set; }

        public Func<T, string>? ParameterTransformator { get; set; }

        public Func<T, bool>? ParameterValidator { get; set; }

        public required Func<List<T>> Values { get; set; }
    }
}
