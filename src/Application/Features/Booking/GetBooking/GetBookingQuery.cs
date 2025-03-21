using Application.Abstraction.Messaging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Booking.GetBooking
{
    public sealed record GetBookingQuery(Guid BookingId) : IQuery<GetBookingQueryResponse>;
    
}
