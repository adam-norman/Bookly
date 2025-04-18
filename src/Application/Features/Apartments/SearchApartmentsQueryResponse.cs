﻿using Application.Abstraction.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Apartments
{
    public sealed class SearchApartmentsQueryResponse
    {

        public Guid Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public decimal Price { get; init; }

        public string Currency { get; init; }

        public AddressResponse Address { get; set; }

    }
}
