using MongoDB.Bson.Serialization.Attributes;
using System;

namespace PayManAPI.Models
{
    public record WorkHourModel
    {
        [BsonId]
        public Guid Id { get; init; }
        public DateTimeOffset WorkStart { get; init; }
        public DateTimeOffset WorkEnd { get; init; }
        public String Description { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
    }
}
