using Domain.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Bookings.Events
{
    public sealed record BookingReservedDomainEvent(Guid BookingId) : IDomainEvent;
}
