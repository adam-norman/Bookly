using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Shared
{
    public record Currency
    {
        public static readonly Currency USD = new Currency("USD");
        public static readonly Currency Euro = new Currency("EUR");
        internal static readonly Currency None = new Currency("");
        public Currency(string code)
        {
            Code = code;
        }
        public string Code { get; init; }
     

        public static readonly IReadOnlyCollection<Currency> All = new List<Currency> { USD, Euro,None };
        public static Currency FromCode(string code) { 
            return All.FirstOrDefault(x => x.Code == code) ?? throw new ApplicationException("Invalid currency code.");
        }
    }
}
