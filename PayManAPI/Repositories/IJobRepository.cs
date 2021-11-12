﻿using PayManAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayManAPI.Repositories
{
    public interface IJobRepository
    {
        Task<JobModel> GetJobAsync(Guid id);
        Task<IEnumerable<JobModel>> GetJobsAsync();
        Task UpdateJobAsync(JobModel job);
        Task DeleteJobAsync(Guid id);
        Task CreateJobAsync(JobModel job);
    }
}
