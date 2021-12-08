using MongoDB.Bson.Serialization.Attributes;
using System;

namespace PayManAPI.Models
{
    public record TaxModel
    {
        [BsonId]
        public Guid Id { get; init; }
        public String TaxName { get; init; }
        public String Description { get; init; }
        public double TaxRate { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
    }
}
