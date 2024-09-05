using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Patient
{
    internal class InvalidPeselException : Exception
    {
        public InvalidPeselException() : base()
        {
        }

        public InvalidPeselException(string? message) : base(message)
        {
        }
    }
}
