using Application.Abstraction.Email;
using Domain.Entities.Bookings;
using Domain.Entities.Bookings.Events;
using Domain.Entities.Users;
using MediatR;

namespace Application.Features.Booking.ReserveBooking
{
    internal sealed partial class BookingReservedDomainEventHandler : INotificationHandler<BookingReservedDomainEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        public BookingReservedDomainEventHandler(IEmailService emailService, IBookingRepository bookingRepository, IUserRepository userRepository)
        {
            _emailService = emailService;
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
        }
        public async Task Handle(BookingReservedDomainEvent notification, CancellationToken cancellationToken)
        {
           var booking = await _bookingRepository.GetByIdAsync(notification.BookingId, cancellationToken);
            if(booking is null)
            {
                throw new InvalidOperationException($"Booking with id {notification.BookingId} not found.");
            }
            var user = await _userRepository.GetByIdAsync(booking.UserId);
            if (user is null)
            {
                throw new InvalidOperationException($"User with id {booking.UserId} not found.");
            }
             await _emailService.SendEmailAsync(user.Email, "Booking Reserved", "Your booking has been reserved and you only have 10 minutes to confirm it.");
        }

    }
}
