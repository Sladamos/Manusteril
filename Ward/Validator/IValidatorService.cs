using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Validator
{
    internal interface IValidatorService
    {
        ValidationResult ValidatePwz(string pwzNumber);
        ValidationResult ValidatePhoneNumber(string phoneNumber);
        ValidationResult ValidatePesel(string pesel);
    }
}
