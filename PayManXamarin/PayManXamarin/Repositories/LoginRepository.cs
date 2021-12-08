using Newtonsoft.Json.Linq;
using PayManXamarin.Authentication;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PayManXamarin.Repositories
{
    public class LoginRepository
    {
        public async Task<AuthenticationResult> AuthenticateLogin(string username, string password)
        {
            string url = AppSettings.baseUrl + "/auth/login";

            HttpClientHandler httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            HttpClient client = new HttpClient(httpClientHandler);

            StringContent content = new StringContent("{ \"userName\": \"" + username + "\"," + "\"password\": \"" + password + "\" }");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response;

            try
            {
                response = await client.PostAsync(url, content);
            } catch(Exception)
            {
                return new AuthenticationResult()
                {
                    IsError = true,
                    Error = "Unable to handle request. Make sure your connection is valid!"
                };
            }

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
