namespace Domain.Entities.Users
{
    public record LastName
    {
        public string Value { get; }
        public LastName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Last name cannot be empty.");
            if (value.Length > 50)
                throw new ArgumentException("Last name cannot exceed 50 characters.");
            Value = value;
        }
        public override string ToString() => Value;
    }
}
