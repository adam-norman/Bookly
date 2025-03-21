using Domain.Entities.Apartments;
using Domain.Entities.Shared;

namespace Domain.Entities.Bookings.Services
{
    public class PricingService
    {
        public PricingDetails CalculatePricing(Apartment apartment, DateRange dateRange)
        {
            var currency = apartment.Price.Currency;
            var totalPeriodInDays = dateRange.LengthInDays;
            var priceForPeriod = new Money(apartment.Price.Amount * totalPeriodInDays, currency);
            decimal percentageUpCharge = 0.0m;
            foreach (var amenity in apartment.Amenities)
            {
                percentageUpCharge += amenity switch
                {
                    Amenity.GardenView or Amenity.MountainView => 0.05m,
                    Amenity.AirConditioning => 0.01m,
                    Amenity.Parking => 0.01m,
                    _ => 0.0m
                };
            }
            var amenitiesUpCharge = Money.Zero();
            var totalCleaningFee = apartment.CleaningFee.Amount;
            if (percentageUpCharge > 0)
            {
                amenitiesUpCharge = new Money(priceForPeriod.Amount * percentageUpCharge, currency);
            }
            var totalPrice = Money.Zero();
            totalPrice = priceForPeriod + amenitiesUpCharge;
            if (apartment.CleaningFee.IsZero()==false)
            {
                totalPrice += new Money(totalCleaningFee, currency);
            }
            return new PricingDetails(priceForPeriod, apartment.CleaningFee, amenitiesUpCharge, totalPrice);
        }
    }
}
