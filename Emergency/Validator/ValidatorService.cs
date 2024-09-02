using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Emergency.Validator
{
    internal class ValidatorService : IValidatorService
    {
        private readonly static int PESEL_LENGTH = 11;

        private readonly static int PWZ_LENGTH = 7;

        public ValidationResult ValidatePwz(string pwzNumber)
        {
            if (pwzNumber.Length != PWZ_LENGTH)
            {
                return new ValidationResult { IsValid = false, ValidatorMessage = "Numer PWZ musi mieć dokładnie 7 cyfr." };
            }

            Regex regex = new Regex("^[1-9][0-9]{6}$");
            bool result = regex.IsMatch(pwzNumber);
            string? communicate = result ? null : "Numer PWZ musi mieć tylko cyfry i nie zaczynać się od zera.";
            return new ValidationResult { IsValid = result, ValidatorMessage = communicate };
        }

        public ValidationResult ValidatePesel(string pesel)
        {
            if (!AreAllDigits(pesel))
            {
                return new ValidationResult { IsValid = false, ValidatorMessage = "Numer PESEL może zawierać tylko cyfry." };
            }

            if (pesel.Length != PESEL_LENGTH)
            {
                return new ValidationResult { IsValid = false, ValidatorMessage = "Numer PESEL musi mieć dokładnie 11 cyfr." };
            }

            if (!HasValidChecksum(pesel))
            {
                return new ValidationResult { IsValid = false, ValidatorMessage = "Niepoprawna suma kontrolna numeru PESEL." };
            }

            return new ValidationResult { IsValid = true };
        }

        private static bool AreAllDigits(string pesel)
        {
            foreach (char c in pesel)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool HasValidChecksum(string pesel)
        {
            int[] weights = [9, 7, 3, 1, 9, 7, 3, 1, 9, 7];
            int sum = 0;

            for (int i = 0; i < weights.Length; i++)
            {
                sum += (int)char.GetNumericValue(pesel[i]) * weights[i];
            }

            int checkDigit = sum % 10;
            return checkDigit == (int)char.GetNumericValue(pesel[10]);
        }
    }
}
