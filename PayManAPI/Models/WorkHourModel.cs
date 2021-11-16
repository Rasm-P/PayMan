using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
