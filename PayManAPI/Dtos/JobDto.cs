﻿using PayManAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayManAPI.Dtos
{
    public class JobDto
    {
        public Guid Id { get; init; }
        public String JobTitle { get; init; }
        public String Description { get; init; }
        public double HourlyWage { get; init; }
        public List<Guid> Taxes { get; init; }
        public List<Guid> WorkHours { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
    }
}
