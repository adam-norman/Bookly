using Application.Abstraction.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Apartments
{
   public sealed record SearchApartmentsQuery(DateOnly StartDate, DateOnly EndDate):IQuery<IReadOnlyList<SearchApartmentsQueryResponse>>;
     
}
