using System;

namespace PayManAPI.Dtos
{
    public class CreateUpdateTaxDto
    {
        public String TaxName { get; init; }
        public String Description { get; init; }
        public double TaxRate { get; init; }
    }
}
