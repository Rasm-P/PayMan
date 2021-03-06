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
    [Route("taxes")]
    public class TaxController : ControllerBase
    {
        private readonly ITaxRepository taxRepository;
        private readonly IJobRepository jobRepository;
        private readonly IUserRepository userRepository;
        private readonly IAuthService authService;

        public TaxController(ITaxRepository taxRepository, IJobRepository jobRepository, IUserRepository userRepository, IAuthService authService)
        {
            this.taxRepository = taxRepository;
            this.jobRepository = jobRepository;
            this.userRepository = userRepository;
            this.authService = authService;
        }

        /// <summary>
        /// Get method returning a list of Taxes from a specific Job as JSON
        /// </summary>
        /// <returns>IEnumerable TaxDto</returns>
        [HttpGet("{jobId}")]
        public async Task<IEnumerable<TaxDto>> GetTaxesAsync(Guid jobId)
        {
            var user = await userRepository.GetuserAsync(Guid.Parse(authService.GetUserIdFromToken(User)));
            if (!user.Jobs.Contains(jobId))
            {
                throw new NotFoundException(jobId);
            }

            var jobs = await jobRepository.GetJobAsync(jobId);
            var taxes = (await taxRepository.GetTaxsAsync(jobs.Taxes)).Select(tax => tax.AsTaxDto()); ;

            return taxes;
        }

        /// <summary>
        /// Get method returning a specific Tax from a specific Job as JSON
        /// </summary>
        /// <returns>TaxDto</returns>
        [HttpGet("{jobId}/{taxId}")]
        public async Task<ActionResult<TaxDto>> GetTaxAsync(Guid jobId, Guid taxId)
        {
            var user = await userRepository.GetuserAsync(Guid.Parse(authService.GetUserIdFromToken(User)));
            var job = await jobRepository.GetJobAsync(jobId);

            if (!user.Jobs.Contains(jobId) || !job.Taxes.Contains(taxId))
            {
                return BadRequest();
            }

            var tax = await taxRepository.GetTaxAsync(taxId);

            if (tax is null)
            {
                return NotFound();
            }
            return tax.AsTaxDto();
        }

        /// <summary>
        /// Post method for creating a Tax for a specific Job
        /// </summary>
        /// <returns>ActionResult CreatedAtAction(name, id, Tax)</returns>
        [HttpPost("{jobId}")]
        public async Task<ActionResult> CreateTaxAsync(CreateUpdateTaxDto taxDto, Guid jobId)
        {
            var user = await userRepository.GetuserAsync(Guid.Parse(authService.GetUserIdFromToken(User)));
            var job = await jobRepository.GetJobAsync(jobId);

            var taxId = Guid.NewGuid();

            TaxModel newTax = new()
            {
                Id = taxId,
                TaxName = taxDto.TaxName,
                Description = taxDto.Description,
                TaxRate = taxDto.TaxRate,
                CreatedAt = DateTimeOffset.Now
            };

            var jobTaxList = job.Taxes;
            jobTaxList.Add(taxId);

            JobModel updateJob = job with
            {
                Taxes = jobTaxList
            };

            await taxRepository.CreateTaxAsync(newTax);
            await jobRepository.UpdateJobAsync(updateJob);

            return CreatedAtAction(nameof(CreateTaxAsync), new { id = newTax.Id }, new { newTax });
        }

        /// <summary>
        /// Put method for updating a specific Tax for a specific Job
        /// </summary>
        /// <returns>ActionResult NoContent</returns>
        [HttpPut("{jobId}/{taxId}")]
        public async Task<ActionResult> UpdateTaxAsync(CreateUpdateTaxDto taxDto, Guid jobId, Guid taxId)
        {
            var user = await userRepository.GetuserAsync(Guid.Parse(authService.GetUserIdFromToken(User)));
            var job = await jobRepository.GetJobAsync(jobId);

            if (!user.Jobs.Contains(jobId) || !job.Taxes.Contains(taxId))
            {
                return BadRequest();
            }

            var taxToUpdate = await taxRepository.GetTaxAsync(taxId);

            if (taxToUpdate is null)
            {
                return NotFound();
            }

            TaxModel updateTax = taxToUpdate with
            {
                TaxName = taxDto.TaxName,
                Description = taxDto.Description,
                TaxRate = taxDto.TaxRate,
            };

            await taxRepository.UpdateTaxAsync(updateTax);

            return NoContent();
        }

        /// <summary>
        /// Delete method for deleting a specific Tax from a specific Job
        /// </summary>
        /// <returns>ActionResult NoContent</returns>
        [HttpDelete("{jobId}/{taxId}")]
        public async Task<ActionResult> DeleteTaxAsync(Guid jobId, Guid taxId)
        {
            var user = await userRepository.GetuserAsync(Guid.Parse(authService.GetUserIdFromToken(User)));
            var job = await jobRepository.GetJobAsync(jobId);

            if (!user.Jobs.Contains(jobId) || !job.Taxes.Contains(taxId))
            {
                return BadRequest();
            }

            var taxToDelete = await taxRepository.GetTaxAsync(taxId);

            if (taxToDelete is null)
            {
                return NotFound();
            }

            var JobTaxIdList = job.Taxes;
            JobTaxIdList.Remove(taxId);

            JobModel updateJob = job with
            {
                Taxes = JobTaxIdList
            };

            await taxRepository.DeleteTaxAsync(taxId);
            await jobRepository.UpdateJobAsync(updateJob);

            return NoContent();
        }
    }
}
