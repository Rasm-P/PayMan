using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayManAPI.Dtos
{
    public class CreateUpdateJobDto
    {
        public String JobTitle { get; init; }
        public String Description { get; init; }
        public double HourlyWage { get; init; }
    }
}
