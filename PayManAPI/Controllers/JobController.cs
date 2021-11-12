using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayManAPI.Dtos;
using PayManAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayManAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("jobs")]
    public class JobController : ControllerBase
    {
        private readonly IJobRepository jobRepository;

        public JobController(IJobRepository jobRepository)
        {
            this.jobRepository = jobRepository;
        }

        //Get /jobs
        [HttpGet]
        public async Task<IEnumerable<JobDto>> GetJobsAsync()
        {
            var jobs = (await jobRepository.GetJobsAsync()).Select(job => job.AsJobDto());
            return jobs;
        }

        //Get /jobs/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<JobDto>> GetJobAsync(Guid id)
        {
            var job = await jobRepository.GetJobAsync(id);

            if (job is null)
            {
                return NotFound();
            }
            return job.AsJobDto();
        }
    }
}
