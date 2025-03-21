using Domain.Entities.Shared;

namespace Domain.Entities.Bookings
{
    public sealed record PricingDetails(Money PriceForPeriod, Money CleaningFee, Money AmenitiesUpCharge, Money TotalPrice);
}