using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayManAPI.Dtos;
using PayManAPI.Exceptions;
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
    [Route("workHours")]
    public class WorkHourController : ControllerBase
    {
        private readonly IWorkHourRepository workHourRepository;
        private readonly IJobRepository jobRepository;
        private readonly IUserRepository userRepository;
        private readonly IAuthService authService;

        public WorkHourController(IWorkHourRepository workHourRepository, IJobRepository jobRepository, IUserRepository userRepository, IAuthService authService)
        {
            this.workHourRepository = workHourRepository;
            this.jobRepository = jobRepository;
            this.userRepository = userRepository;
            this.authService = authService;
        }

        //Get /workHours/{id}
        [HttpGet("{jobId}")]
        public async Task<IEnumerable<WorkHourDto>> GetWorkHoursAsync(Guid jobId)
        {
            var user = await userRepository.GetuserAsync(Guid.Parse(authService.GetUserIdFromToken(User)));
            if (!user.Jobs.Contains(jobId))
            {
                //return BadRequest();
                throw new NotFoundException(jobId);
            }

            var jobs = await jobRepository.GetJobAsync(jobId);
            var workHours = (await workHourRepository.GetWorkHoursAsync(jobs.WorkHours)).Select(x => x.AsWorkHourDto()); ;

            return workHours;
        }

        //Get /workHours/{id}/{id}
        [HttpGet("{jobId}/{workHourId}")]
        public async Task<ActionResult<WorkHourDto>> GetWorkHourAsync(Guid jobId, Guid workHourId)
        {
            var user = await userRepository.GetuserAsync(Guid.Parse(authService.GetUserIdFromToken(User)));
            var job = await jobRepository.GetJobAsync(jobId);

            if (!user.Jobs.Contains(jobId) || !job.WorkHours.Contains(workHourId))
            {
                return BadRequest();
            }

            var workHour = await workHourRepository.GetWorkHourAsync(workHourId);

            if (workHour is null)
            {
                return NotFound();
            }
            return workHour.AsWorkHourDto();
        }

        //Post /workHours/{id}
        [HttpPost("{jobId}")]
        public async Task<ActionResult> CreateWorkHourAsync(CreateUpdateWorkHourDto workHourDto, Guid jobId)
        {
            var user = await userRepository.GetuserAsync(Guid.Parse(authService.GetUserIdFromToken(User)));
            var job = await jobRepository.GetJobAsync(jobId);

            var workHourId = Guid.NewGuid();

            WorkHourModel newWorkHour = new()
            {
                Id = workHourId,
                Description = workHourDto.Description,
                WorkStart = workHourDto.WorkStart,
                WorkEnd = workHourDto.WorkEnd,
                CreatedAt = DateTimeOffset.Now
            };

            var jobWorkHourList = job.WorkHours;
            jobWorkHourList.Add(workHourId);

            JobModel updateJob = job with
            {
                WorkHours = jobWorkHourList
            };

            await workHourRepository.CreateWorkHourAsync(newWorkHour);
            await jobRepository.UpdateJobAsync(updateJob);

            return CreatedAtAction(nameof(CreateWorkHourAsync), new { id = newWorkHour.Id }, new { newWorkHour });
        }

        //Put /workHours/{id}/{id}
        [HttpPut("{jobId}/{workHourId}")]
        public async Task<ActionResult> UpdateWorkHourAsync(CreateUpdateWorkHourDto workHourDto, Guid jobId, Guid workHourId)
        {
            var user = await userRepository.GetuserAsync(Guid.Parse(authService.GetUserIdFromToken(User)));
            var job = await jobRepository.GetJobAsync(jobId);

            if (!user.Jobs.Contains(jobId) || !job.WorkHours.Contains(workHourId))
            {
                return BadRequest();
            }

            var workHourToUpdate = await workHourRepository.GetWorkHourAsync(workHourId);

            if (workHourToUpdate is null)
            {
                return NotFound();
            }

            WorkHourModel updateWorkHour = workHourToUpdate with
            {
                Description = workHourDto.Description,
                WorkStart = workHourDto.WorkStart,
                WorkEnd = workHourDto.WorkEnd
            };

            await workHourRepository.UpdateWorkHourAsync(updateWorkHour);

            return NoContent();
        }

        //Delete /workHours/{id}/{id}
        [HttpDelete("{jobId}/{workHourId}")]
        public async Task<ActionResult> DeleteWorkHourAsync(Guid jobId, Guid workHourId)
        {
            var user = await userRepository.GetuserAsync(Guid.Parse(authService.GetUserIdFromToken(User)));
            var job = await jobRepository.GetJobAsync(jobId);

            if (!user.Jobs.Contains(jobId) || !job.WorkHours.Contains(workHourId))
            {
                return BadRequest();
            }

            var workHourToDelete = await workHourRepository.GetWorkHourAsync(workHourId);

            if (workHourToDelete is null)
            {
                return NotFound();
            }

            var JobWorkHourIdList = job.WorkHours;
            JobWorkHourIdList.Remove(workHourId);

            JobModel updateJob = job with
            {
                WorkHours = JobWorkHourIdList
            };

            await workHourRepository.DeleteWorkHourAsync(workHourId);
            await jobRepository.UpdateJobAsync(updateJob);

            return NoContent();
        }
    }
}
