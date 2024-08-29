using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Validator
{
    internal class ValidatorService : IValidatorService
    {
        private readonly static int PESEL_LENGTH = 11;

        public ValidationResult validatePesel(string pesel)
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
