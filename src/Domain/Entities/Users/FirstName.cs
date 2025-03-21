namespace Domain.Entities.Users
{
    public record FirstName 
    {
        public string Value { get; }
        public FirstName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("First name cannot be empty.");
            if (value.Length > 50)
                throw new ArgumentException("First name cannot exceed 50 characters.");
            Value = value;
        }
        public override string ToString() => Value;
    }
}
