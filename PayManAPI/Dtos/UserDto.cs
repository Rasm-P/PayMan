using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
