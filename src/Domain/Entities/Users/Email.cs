using System.Text.RegularExpressions;

namespace Domain.Entities.Users
{
    public sealed record Email
    {
        private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

        public string Value { get; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(value));
            }

            if (!EmailRegex.IsMatch(value))
            {
                throw new ArgumentException("Invalid email format.", nameof(value));
            }

            Value = value;
        }

        public override string ToString() => Value;
    }
}
