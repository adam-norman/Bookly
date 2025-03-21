using Domain.Entities.Abstractions;

namespace Domain.Entities.Bookings.Events
{
    public sealed record BookingRejectedDomainEvent(Guid BookingId) : IDomainEvent;
    
}
