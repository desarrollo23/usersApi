using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using User.Common.Response;
using User.Infraestructure.Base.Context;
using User.Model.DTOs;
using User.Model.Http.Response;
using User.Model.Interfaces.Engine;
using User.Model.Models;

namespace User.Service.User
{
    public class HostedUserService : IHostedService
    {
        private Timer timer;
        private readonly IServiceScopeFactory _scopeFactory;

        public HostedUserService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

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

        private async Task<HttpApiResponse> GetUsersFromApi(int pageNumber)
        {
            using var httpClient = new HttpClient();

            using var response = await httpClient.GetAsync($"https://reqres.in/api/users?page={pageNumber}");
            string apiResponse = await response.Content.ReadAsStringAsync();

            HttpApiResponse httpApiResponse = JsonConvert.DeserializeObject<HttpApiResponse>(apiResponse);

            return httpApiResponse;
        }

        private void SaveUserData(List<UserApi> userEntities)
        {
            try
            {
                var users = MapUsers(userEntities);

                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<UserContext>();
                dbContext.Users.AddRange(users);
                dbContext.SaveChanges();
            }
            catch (Exception)
            {
                
            }
        }

        private void DoWork(object state)
        {
            int pageNumber = 1;
            bool continueCallingApi = true;

            while (continueCallingApi)
            {
                var response = GetUsersFromApi(pageNumber).Result;

                if (response.Data.Any())
                {
                    SaveUserData(response.Data);

                    if (pageNumber == response.Total_Pages)
                        continueCallingApi = false;

                    pageNumber++;
                }

            }
        }

        private List<UserEntity> MapUsers(List<UserApi> userApis)
        {
            var users = new List<UserEntity>();

            userApis.ForEach(x =>
            {
                users.Add(new UserEntity { FirstName = x.First_Name, LastName = x.Last_Name, Avatar = x.Avatar, Email = x.Email});
            });

            return users;
        }
    }
}
