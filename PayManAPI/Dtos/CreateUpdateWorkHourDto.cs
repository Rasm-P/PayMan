using System;

namespace PayManAPI.Dtos
{
    public class CreateUpdateWorkHourDto
    {
        public DateTimeOffset WorkStart { get; init; }
        public DateTimeOffset WorkEnd { get; init; }
        public String Description { get; init; }
    }
}
