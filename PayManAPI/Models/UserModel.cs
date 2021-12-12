using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace PayManAPI.Models
{
    public record UserModel
    {
        [BsonId]
        public Guid Id { get; init; }
        public String UserName { get; init; }
        public String Password { get; init; }
        public List<Guid> Jobs { get; init; }
        public double Frikort { get; init; }
        public double Hovedkort { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
    }
}
