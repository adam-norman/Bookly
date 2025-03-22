using Application.Abstraction.Clock;
using Application.Abstraction.Exceptions;
using Application.Abstraction.Messaging;
using Domain;
using Domain.Entities.Abstractions;
using Domain.Entities.Apartments;
using Domain.Entities.Bookings;
using Domain.Entities.Bookings.Services;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Booking.ReserveBooking
{
    internal sealed partial class ReserveBookingHandler : ICommandHandler<ReserveBookingCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookingRepository _bookingRepository;
        private readonly PricingService _pricingService;
        private readonly IDateTimeProvider _dateTimeProvider;
        public ReserveBookingHandler(IUserRepository userRepository, 
            IApartmentRepository apartmentRepository, 
            IUnitOfWork unitOfWork, 
            IBookingRepository bookingRepository, 
            PricingService pricingService,
            IDateTimeProvider dateTimeProvider)
        {
            _userRepository = userRepository;
            _apartmentRepository = apartmentRepository;
            _unitOfWork = unitOfWork;
            _bookingRepository = bookingRepository;
            _pricingService = pricingService;
            _dateTimeProvider= dateTimeProvider;
        }

        public async Task<Result<Guid>> Handle(ReserveBookingCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

            if (user is null)
            {
                return Result.Failure<Guid>(UserErrors.NotFound);
            }

            var apartment = await _apartmentRepository.GetByIdAsync(request.ApartmentId, cancellationToken);

            if (apartment is null)
            {
                return Result.Failure<Guid>(ApartmentErrors.NotFound);
            }

            var duration = DateRange.Create(request.StartDate, request.EndDate);

            if (await _bookingRepository.IsOverlappingAsync(apartment, duration, cancellationToken))
            {
                return Result.Failure<Guid>(BookingErrors.Overlap);
            }

            try
            {
                var booking =Domain.Entities.Bookings.Booking.Reserve(
                    apartment,
                    user.Id,
                    duration,
                    _dateTimeProvider.UtcNow,
                    _pricingService);

                _bookingRepository.Add(booking);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return booking.Id;
            }
            catch (ConcurrencyException)
            {
                return Result.Failure<Guid>(BookingErrors.Overlap);
            }
        }
    }
}
