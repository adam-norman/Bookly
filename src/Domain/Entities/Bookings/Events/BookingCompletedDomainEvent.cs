using Domain.Entities.Abstractions;

namespace Domain.Entities.Bookings.Events
{
    public sealed record BookingCompletedDomainEvent(Guid BookingId) : IDomainEvent;
    
}
