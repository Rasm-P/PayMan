using System;

namespace PayManAPI.Dtos
{
    public class WorkHourDto
    {
        public Guid Id { get; init; }
        public DateTimeOffset WorkStart { get; init; }
        public DateTimeOffset WorkEnd { get; init; }
        public String Description { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
    }
}
