using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
