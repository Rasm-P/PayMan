using Newtonsoft.Json;
using PayManXamarin.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace PayManXamarin.Repositories
{
    public class JobsRepository
    {
        public async Task<List<JobModel>> GetJobsAsync()
        {
            string url = AppSettings.baseUrl + "/jobs";

            HttpClientHandler httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            HttpClient client = new HttpClient(httpClientHandler);

            string token = await SecureStorage.GetAsync("accessToken");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response;

            try
            {
                response = await client.GetAsync(url);
            }
            catch (Exception)
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string responseString = await response.Content.ReadAsStringAsync();

            List<JobModel> jobs = JsonConvert.DeserializeObject<List<JobModel>>(responseString);
            return jobs;
        }
    }
}
