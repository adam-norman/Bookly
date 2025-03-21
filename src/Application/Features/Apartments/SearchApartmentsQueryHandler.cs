using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Dapper;
using Domain.Entities.Abstractions;
using Domain.Entities.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Apartments
{
    internal sealed class SearchApartmentsQueryHandler : IQueryHandler<SearchApartmentsQuery, IReadOnlyList<SearchApartmentsQueryResponse>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private static readonly int[] ActiveBookingStatuses =
            {
                (int)BookingStatus.Reserved,
                (int)BookingStatus.Confirmed,
                (int)BookingStatus.Completed
            };
        public SearchApartmentsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<Result<IReadOnlyList<SearchApartmentsQueryResponse>>> Handle(SearchApartmentsQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.CreateConnection();
            const string sql = """
            SELECT
                a.id AS Id,
                a.name AS Name,
                a.description AS Description,
                a.price_amount AS Price,
                a.price_currency AS Currency,
                a.address_country AS Country,
                a.address_state AS State,
                a.address_zip_code AS ZipCode,
                a.address_city AS City,
                a.address_street AS Street
            FROM apartments AS a
            WHERE NOT EXISTS
            (
                SELECT 1
                FROM bookings AS b
                WHERE
                    b.apartment_id = a.id AND
                    b.duration_start <= @EndDate AND
                    b.duration_end >= @StartDate AND
                    b.status = ANY(@ActiveBookingStatuses)
            )
            """;
            var apartments = await connection
            .QueryAsync<SearchApartmentsQueryResponse, AddressResponse, SearchApartmentsQueryResponse>(
                sql,
                (apartment, address) =>
                {
                    apartment.Address = address;

                    return apartment;
                },
                new
                {
                    request.StartDate,
                    request.EndDate,
                    ActiveBookingStatuses
                },
                splitOn: "Country");

            return apartments.ToList();
        }
    }
}
