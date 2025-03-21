using Application.Abstraction.Messaging;
using Domain.Entities.Apartments;
using Domain.Entities.Bookings.Services;
using Domain.Entities.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.Features.Booking.ReserveBooking
{
    public record ReserveBookingCommand(Guid ApartmentId, Guid UserId, DateOnly StartDate, DateOnly EndDate) : ICommand<Guid> { }
    
}
