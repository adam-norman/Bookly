namespace Api.Controllers.Bookings
{
    public sealed record ReserveBookingRequest  
    {
        public Guid ApartmentId { get; init; }
        public Guid UserId { get; init; }
        public DateOnly StartDate { get; init; }
        public DateOnly EndDate { get; init; }
    }

}
