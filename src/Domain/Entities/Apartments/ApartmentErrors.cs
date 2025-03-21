using Domain.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Apartments
{
     public static class ApartmentErrors
    {
        public static Error NotFound = new(
            "Apartment.Found",
            "The Apartment with the specified identifier was not found");
    }
}
