using Domain.Entities.Abstractions;
using Domain.Entities.Apartments;
using Domain.Entities.Bookings.Events;
using Domain.Entities.Bookings.Services;
using Domain.Entities.Shared;

namespace Domain.Entities.Bookings
{
    public sealed class Booking : Entity
    {
        private Booking(Guid id, Guid apartmentId = default, Guid userId = default, DateRange duration = null, Money priceForPeriod = null, Money cleaningFee = null, Money amenitiesUpCharge = null, Money totalPrice = null, BookingStatus status = default, DateTime createOnUtc = default, DateTime? confirmedOnUtc = null, DateTime? rejectedOnUtc = null, DateTime? cancelledOnUtc = null, DateTime? completedOnUtc = null) : base(id)
        {
            ApartmentId = apartmentId;
            UserId = userId;
            Duration = duration;
            PriceForPeriod = priceForPeriod;
            CleaningFee = cleaningFee;
            AmenitiesUpCharge = amenitiesUpCharge;
            TotalPrice = totalPrice;
            Status = status;
            CreateOnUtc = createOnUtc;
            ConfirmedOnUtc = confirmedOnUtc;
            RejectedOnUtc = rejectedOnUtc;
            CancelledOnUtc = cancelledOnUtc;
            CompletedOnUtc = completedOnUtc;
        }
        public Guid ApartmentId { get; private set; }
        public Guid UserId { get; private set; }
        public DateRange Duration { get; private set; }
        public Money PriceForPeriod { get; private set; }
        public Money CleaningFee { get; private set; }
        public Money AmenitiesUpCharge { get; private set; }
        public Money TotalPrice { get; private set; }
        public BookingStatus Status { get; private set; }
        public DateTime CreateOnUtc { get; private set; }
        public DateTime? ConfirmedOnUtc { get; private set; }
        public DateTime? RejectedOnUtc { get; private set; }
        public DateTime? CancelledOnUtc { get; private set; }
        public DateTime? CompletedOnUtc { get; private set; }
        public static Booking Reserve(Guid id, Apartment apartment, Guid userId, DateRange duration,
            DateTime utcNow, PricingService pricingService)
        {
            var pricingDetails = pricingService.CalculatePricing(apartment, duration);
            var booking = new Booking(Guid.NewGuid(), apartment.Id, userId, duration,
                pricingDetails.PriceForPeriod, pricingDetails.CleaningFee, pricingDetails.AmenitiesUpCharge, pricingDetails.TotalPrice,
                BookingStatus.Reserved, utcNow);
            apartment.LastTimeBookedOnUtc = utcNow;
            booking.RaiseDomainEvent(new BookingReservedDomainEvent(booking.Id));
            return booking;

        }
        public Result Confirm(DateTime utcNow)
        {
            if (Status != BookingStatus.Reserved)
            {
                return Result.Failure(BookingErrors.NotReserved);
            }
            Status = BookingStatus.Confirmed;
            ConfirmedOnUtc = utcNow;
            RaiseDomainEvent(new BookingConfirmedDomainEvent(Id));
            return Result.Success();
        }
        public Result Reject(DateTime utcNow)
        {
            if (Status != BookingStatus.Reserved)
            {
                return Result.Failure(BookingErrors.NotReserved);
            }
            Status = BookingStatus.Rejected;
            RejectedOnUtc = utcNow;
            RaiseDomainEvent(new BookingRejectedDomainEvent(Id));
            return Result.Success();
        }
        public Result Cancel(DateTime utcNow)
        {
            if (Status != BookingStatus.Confirmed)
            {
                return Result.Failure(BookingErrors.NotConfirmed);
            }
            var currentDate = DateOnly.FromDateTime(utcNow);
            if (currentDate > Duration.Start)
            {
                return Result.Failure(BookingErrors.AlreadyStarted);
            }
            Status = BookingStatus.Cancelled;
            CancelledOnUtc = utcNow;
            RaiseDomainEvent(new BookingCancelledDomainEvent(Id));
            return Result.Success();
        }
        public Result Complete(DateTime utcNow)
        {
            if (Status != BookingStatus.Confirmed)
            {
                return Result.Failure(BookingErrors.NotConfirmed);
            }
            Status = BookingStatus.Completed;
            CompletedOnUtc = utcNow;
            RaiseDomainEvent(new BookingCompletedDomainEvent(Id));
            return Result.Success();
        }
    }
}
