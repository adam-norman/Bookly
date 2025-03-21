using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Shared
{
    public record  Money(decimal Amount, Currency Currency)
    {
        public static Money operator +(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new ApplicationException("Cannot add money with different currencies.");
            return new Money(a.Amount + b.Amount, a.Currency);
        }
        public static Money operator -(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new ApplicationException("Cannot subtract money with different currencies.");
            return new Money(a.Amount - b.Amount, a.Currency);
        }
        public static Money Zero() 
        { 
            return new Money(0, Currency.None); 
        }
        public  bool IsZero() => this == Zero(Currency);

        private Money Zero(Currency currency)
        {
            return new Money(0, currency);
        }
    }
}
