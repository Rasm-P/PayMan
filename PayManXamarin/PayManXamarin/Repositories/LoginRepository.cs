using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PayManXamarin.Authentication;
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
        public async Task<AuthenticationResult> AuthenticateLogin(string username, string password)
        {
            string uri = "https://10.0.2.2:5001/auth/login";

            HttpClientHandler httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            HttpClient client = new HttpClient(httpClientHandler);

            StringContent content = new StringContent("{ \"userName\": \"" + username + "\"," + "\"password\": \"" + password + "\" }");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                return new AuthenticationResult()
                {
                    IsError = true,
                    Error = await response.Content.ReadAsStringAsync()
                };
            }

            string responseString = await response.Content.ReadAsStringAsync();
            JObject jsonObjcet = JObject.Parse(responseString);
            string token = jsonObjcet["token"].ToString();
            string user = jsonObjcet["user"].ToString();

            return new AuthenticationResult()
            {
                AccessToken = token,
                User = user,
                IsError = false
            };
        }
    }
}
