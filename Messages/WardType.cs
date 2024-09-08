using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public enum WardType
    {
        NONE,
        GENERAL,
        PAEDIATRICS,
        SURGERY,
        PSYCHIATRIC,
        CARDIOLOGY,
        PULMONOLOGY,
        ICU,
        UROLOGY
    }

    public static class WardTypeExtensions
    {
        public static string ToPolish(this WardType wardType)
        {
            return wardType switch
            {
                WardType.NONE => "Brak",
                WardType.GENERAL => "Ogólny",
                WardType.PAEDIATRICS => "Pediatria",
                WardType.SURGERY => "Chirurgia",
                WardType.PSYCHIATRIC => "Psychiatryczny",
                WardType.CARDIOLOGY => "Kardiologia",
                WardType.PULMONOLOGY => "Pulmonologia",
                WardType.ICU => "OIOM",
                WardType.UROLOGY => "Urologia",
                _ => throw new ArgumentOutOfRangeException(nameof(wardType), wardType, null)
            };
        }
        
        public static WardType FromPolish(string polishName)
        {
            return polishName switch
            {
                "Brak" => WardType.NONE,
                "Ogólny" => WardType.GENERAL,
                "Pediatria" => WardType.PAEDIATRICS,
                "Chirurgia" => WardType.SURGERY,
                "Psychiatryczny" => WardType.PSYCHIATRIC,
                "Kardiologia" => WardType.CARDIOLOGY,
                "Pulmonologia" => WardType.PULMONOLOGY,
                "OIOM" => WardType.ICU,
                "Urologia" => WardType.UROLOGY,
                _ => throw new ArgumentException($"Nieznana nazwa oddziału: {polishName}", nameof(polishName))
            };
        }
    }
}
