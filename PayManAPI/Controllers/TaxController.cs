using Microsoft.AspNetCore.Authorization;
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

        //Get /taxes/{id}
        [HttpGet("{id}")]
        public async Task<IEnumerable<TaxDto>> GetTaxesAsync(Guid jobId)
        {
            var user = await userRepository.GetuserAsync(Guid.Parse(authService.GetUserIdFromToken(User)));
            if (!user.Jobs.Contains(jobId))
            {
                //return Unauthorized();
                return null;
            }

            var jobs = await jobRepository.GetJobAsync(jobId);
            var taxes = (await taxRepository.GetTaxsAsync(jobs.Taxes)).Select(tax => tax.AsTaxDto()); ;

            return taxes;
        }

        //Get /taxes/{id}
        [HttpGet("{jobId}/{taxId}")]
        public async Task<ActionResult<TaxDto>> GetTaxAsync(Guid jobId, Guid taxId)
        {
            var user = await userRepository.GetuserAsync(Guid.Parse(authService.GetUserIdFromToken(User)));
            var job = await jobRepository.GetJobAsync(jobId);

            if (!user.Jobs.Contains(jobId) || !job.Taxes.Contains(taxId))
            {
                return Unauthorized();
            }

            var tax = await taxRepository.GetTaxAsync(taxId);

            if (tax is null)
            {
                return NotFound();
            }
            return tax.AsTaxDto();
        }

        //Post /taxes/{id}
        [HttpPost("{id}")]
        public async Task<ActionResult> CreateJobAsync(CreateUpdateTaxDto taxDto, Guid id)
        {
            var user = await userRepository.GetuserAsync(Guid.Parse(authService.GetUserIdFromToken(User)));
            var job = await jobRepository.GetJobAsync(id);

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

            return CreatedAtAction(nameof(CreateJobAsync), new { id = newTax.Id }, new { newTax });
        }
    }
}
