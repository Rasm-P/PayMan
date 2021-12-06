using System;
using System.Collections.Generic;
using System.Text;

namespace PayManXamarin.Models
{
    public class JobModel
    {
        public Guid Id { get; set; }
        public String JobTitle { get; set; }
        public String Description { get; set; }
        public double HourlyWage { get; set; }
        public List<Guid> Taxes { get; set; }
        public List<Guid> WorkHours { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
