using System;

namespace PayManAPI.Dtos
{
    //The dto object that we will be exposing to users of our api
    public record UserDto
    {
        public Guid Id { get; init; }
        public String UserName { get; init; }
        public double Frikort { get; init; }
        public double Hovedkort { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
    }
}
