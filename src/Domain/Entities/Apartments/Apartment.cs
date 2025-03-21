using Domain.Entities.Abstractions;
using Domain.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Apartments
{
    public sealed class Apartment : Entity
    {
        public Apartment(Guid id, Name name, Description description, Address address, Money price, Money cleaningFee, DateTime? lastTimeBookedOnUtc, List<Amenity> amenities) : base(id)
        {
            Name = name;
            Description = description;
            Address = address;
            Price = price;
            CleaningFee = cleaningFee;
            LastTimeBookedOnUtc = lastTimeBookedOnUtc;
            Amenities = amenities;
        }
        public Name Name { get; private set; }
        public Description Description { get; private set; }
        public Address Address { get; private set; }
        public Money Price { get; private set; }
        public Money CleaningFee { get;private set; }
        public DateTime? LastTimeBookedOnUtc { get; internal set; }
        public List<Amenity> Amenities { get; private set; } = new List<Amenity>();

    }
}
