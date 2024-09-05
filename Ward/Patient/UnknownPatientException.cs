using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Patient
{
    internal class UnknownPatientException : Exception
    {
        public UnknownPatientException(string? message) : base(message)
        {
        }
    }
}
