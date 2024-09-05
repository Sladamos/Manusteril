using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Validator
{
    internal class ValidationResult
    {
        public required bool IsValid { get; set; }

        public string? ValidatorMessage { get; set; }
    }
}
