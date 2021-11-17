using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayManAPI.Dtos
{
    public class TaxDto
    {
        public Guid Id { get; init; }
        public String TaxName { get; init; }
        public String Description { get; init; }
        public double TaxRate { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
    }
}
