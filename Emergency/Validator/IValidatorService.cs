using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Validator
{
    internal interface IValidatorService
    {
        ValidationResult validatePesel(string pesel);
    }
}
