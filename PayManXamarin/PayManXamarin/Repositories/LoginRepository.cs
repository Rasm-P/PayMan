using Newtonsoft.Json;
using PayManXamarin.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PayManXamarin.Repositories
{
    public class LoginRepository
    {
        public async Task<Boolean> AuthenticateLogin(string username, string password)
        {
            string uri = "https://10.0.2.2:5001/auth/login";

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            HttpClient client = new HttpClient(httpClientHandler);

            //HttpClient client = new HttpClient();

            StringContent content = new StringContent("{" + "  \"userName\": \"New2\"," + "\"password\": \"1234\"" + "}");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);

            
            string serialized = await response.Content.ReadAsStringAsync();
            //await HandleResponse(response);


            /*
            TResult result = await Task.Run(() =>
            JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));
            */

            return true;
        }

        private async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception(content);
                }
                throw new HttpRequestException(content);
            }
        }
    }
}
