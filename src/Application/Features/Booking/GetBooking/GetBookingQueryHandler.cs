﻿using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Dapper;
using Domain.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Booking.GetBooking
{
    internal sealed class GetBookingQueryHandler : IQueryHandler<GetBookingQuery, GetBookingQueryResponse>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetBookingQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<GetBookingQueryResponse>> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            var connection=_sqlConnectionFactory.CreateConnection();
            const string sql = """
            SELECT
                id AS Id,
                apartment_id AS ApartmentId,
                user_id AS UserId,
                status AS Status,
                price_for_period_amount AS PriceAmount,
                price_for_period_currency AS PriceCurrency,
                cleaning_fee_amount AS CleaningFeeAmount,
                cleaning_fee_currency AS CleaningFeeCurrency,
                amenities_up_charge_amount AS AmenitiesUpChargeAmount,
                amenities_up_charge_currency AS AmenitiesUpChargeCurrency,
                total_price_amount AS TotalPriceAmount,
                total_price_currency AS TotalPriceCurrency,
                duration_start AS DurationStart,
                duration_end AS DurationEnd,
                created_on_utc AS CreatedOnUtc
            FROM bookings
            WHERE id = @BookingId
            """;
            var booking = await connection.QueryFirstOrDefaultAsync<GetBookingQueryResponse>(sql,new { BookingId= request.BookingId });
            return booking;
        }
    }
}
