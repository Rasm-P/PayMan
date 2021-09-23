using System;

namespace PayManAPI.Models
{
    public record User
    {
        public Guid Id { get; init; }
        public String UserName { get; init; }
        
        public String Password { get; init; }

        //public List<Jobs>
        public double Frikort { get; init; }
        public double Hovedkort { get; init; }
        //public Enum? Nationality { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
    }
}
