using PayManXamarin.Models;
using PayManXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PayManXamarin.Views
{
    public partial class JobsPage : ContentPage
    {
        private ObservableCollection<JobModel> _jobs;
        private readonly JobsViewModel jobsViewModel;

        public JobsPage()
        {
            InitializeComponent();
            jobsViewModel = new JobsViewModel();
        }

        protected override async void OnAppearing()
        {
            List<JobModel> jobs = await jobsViewModel.GetJobsAsync();
            if (jobs != null)
            {
                _jobs = new ObservableCollection<JobModel>(jobs);
                JobList.ItemsSource = _jobs;

                if (!jobs.Any())
                {
                    Add_new_job.IsVisible = true;
                    JobList.IsVisible = false;
                }
            }
            else
            {
                Unable_to_connect.IsVisible = true;
                JobList.IsVisible = false;
            }
            base.OnAppearing();
        }
    }
}
