﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayManAPI.Dtos;
using PayManAPI.Models;
using PayManAPI.Repositories;
using PayManAPI.Security;
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
        private readonly IUserRepository userRepository;
        private readonly IAuthService authService;

        public JobController(IJobRepository jobRepository, IUserRepository userRepository, IAuthService authService)
        {
            this.jobRepository = jobRepository;
            this.userRepository = userRepository;
            this.authService = authService;
        }

        //Get /jobs
        [HttpGet]
        public async Task<IEnumerable<JobDto>> GetJobsAsync()
        {
            var user = await userRepository.GetuserAsync(Guid.Parse(authService.GetUserIdFromToken(User)));

            var jobs = (await jobRepository.GetJobsAsync(user.Jobs)).Select(job => job.AsJobDto());
            return jobs;
        }

        //Get /jobs/{id}
        [HttpGet("{jobId}")]
        public async Task<ActionResult<JobDto>> GetJobAsync(Guid jobId)
        {
            var user = await userRepository.GetuserAsync(Guid.Parse(authService.GetUserIdFromToken(User)));
            if (!user.Jobs.Contains(jobId))
            {
                return Unauthorized();
            }

            var job = await jobRepository.GetJobAsync(jobId);

            if (job is null)
            {
                return NotFound();
            }
            return job.AsJobDto();
        }

        //Post /jobs
        [HttpPost]
        public async Task<ActionResult> CreateJobAsync(CreateUpdateJobDto jobDto)
        {
            var user = await userRepository.GetuserAsync(Guid.Parse(authService.GetUserIdFromToken(User)));

            var jobId = Guid.NewGuid();

            JobModel newjob = new()
            {
                Id = jobId,
                JobTitle = jobDto.JobTitle,
                Description = jobDto.Description,
                HourlyWage = jobDto.HourlyWage,
                Taxes = new List<Guid>(),
                WorkHours = new List<Guid>(),
                CreatedAt = DateTimeOffset.Now
            };

            var userJobIdList = user.Jobs;
            userJobIdList.Add(jobId);

            UserModel updateUser = user with
            {
                Jobs = userJobIdList
            };

            await jobRepository.CreateJobAsync(newjob);
            await userRepository.UpdateUserAsync(updateUser);

            return CreatedAtAction(nameof(CreateJobAsync), new { id = newjob.Id }, new { newjob });
        }

        //Put /jobs/{id}
        [HttpPut("{jobId}")]
        public async Task<ActionResult> UpdateJobAsync(CreateUpdateJobDto jobDto, Guid jobId)
        {
            var user = await userRepository.GetuserAsync(Guid.Parse(authService.GetUserIdFromToken(User)));
            if (!user.Jobs.Contains(jobId))
            {
                return Unauthorized();
            }

            var jobToUpdate = await jobRepository.GetJobAsync(jobId);

            if (jobToUpdate is null)
            {
                return NotFound();
            }

            JobModel updateJob = jobToUpdate with
            {
                JobTitle = jobDto.JobTitle,
                Description = jobDto.Description,
                HourlyWage = jobDto.HourlyWage
            };

            await jobRepository.UpdateJobAsync(updateJob);

            return NoContent();
        }

        //Delete /jobs/{id}
        [HttpDelete("{jobId}")]
        public async Task<ActionResult> DeleteJobAsync(Guid jobId)
        {
            var user = await userRepository.GetuserAsync(Guid.Parse(authService.GetUserIdFromToken(User)));
            if (!user.Jobs.Contains(jobId))
            {
                return Unauthorized();
            }

            var jobToDelte = await jobRepository.GetJobAsync(jobId);

            if (jobToDelte is null)
            {
                return NotFound();
            }

            var userJobIdList = user.Jobs;
            userJobIdList.Remove(jobId);

            UserModel updateUser = user with
            {
                Jobs = userJobIdList
            };

            await jobRepository.DeleteJobAsync(jobId);
            await userRepository.UpdateUserAsync(updateUser);

            return NoContent();
        }
    }
}
