using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Apartments
{
   public record Name
    {
        public string Value { get;}
        public Name(string value)  
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be empty.");
            if (value.Length > 100)
                throw new ArgumentException("Name cannot exceed 100 characters.");

            Value = value;
        }
        public override string ToString() => Value;

    }

}
