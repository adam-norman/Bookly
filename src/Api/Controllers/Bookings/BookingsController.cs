using Application.Features.Booking.GetBooking;
using Application.Features.Booking.ReserveBooking;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Bookings
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly ISender _sender;

        public BookingsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(Guid id, CancellationToken cancellationToken)
        {
            var getBookingQuery = new GetBookingQuery(id);
            var result = await _sender.Send(getBookingQuery, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ReserveBooking(ReserveBookingRequest reserveBookingRequest, 
            CancellationToken cancellationToken)
        {
            var reserveBookingCommand = new ReserveBookingCommand(reserveBookingRequest.ApartmentId,
                reserveBookingRequest.UserId, reserveBookingRequest.StartDate,
                reserveBookingRequest.EndDate);
            var result = await _sender.Send(reserveBookingCommand, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return CreatedAtAction(nameof(GetBooking), new { id = result.Value }, result.Value);
        }

    }

}
