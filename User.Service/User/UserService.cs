using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using User.Model.Http.Response;
using User.Model.Models;

namespace User.Service.User
{
    public class UserService : IHostedService
    {
        private Timer timer;
        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(24));

            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Dispose();
            return Task.CompletedTask;
        }

        private async Task<HttpApiResponse> GetUsersFromApi()
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync("https://reqres.in/api/users?page=1");
            string apiResponse = await response.Content.ReadAsStringAsync();

            HttpApiResponse httpApiResponse = JsonConvert.DeserializeObject<HttpApiResponse>(apiResponse);

            return httpApiResponse;
        }

        private void SaveUserInfo()
        {

        }

        private void DoWork(object state)
        {

        }
    }
}
