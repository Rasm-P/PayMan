using MvvmHelpers;
using PayManXamarin.Models;
using PayManXamarin.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayManXamarin.ViewModels
{
    public class JobsViewModel : BaseViewModel
    {
        private readonly JobsRepository _jobsRepository = new JobsRepository();

        public async Task<List<JobModel>> GetJobsAsync()
        {
            List<JobModel> jobs = await _jobsRepository.GetJobsAsync();
            return jobs;
        }


    }
}
