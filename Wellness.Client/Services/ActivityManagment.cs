using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Model;
using Moq;
using System.IO;
using AutoMapper;
using RestSharp;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using System.Text.Json.Serialization;

namespace Wellness.Client.Services
{
    public class ActivityManagment : IActivityManagementService
    {
        private readonly HttpClient _httpClient;
        public ActivityManagment(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Create(Activity activity)
        {
            await _httpClient.PostJsonAsync($"api/activities", activity);
        }
        public async Task Disable(Guid activityId)
        {
            await _httpClient.DeleteAsync($"api/activities/{activityId}");
        }

        public async Task<IEnumerable<Activity>> GetAll()
        {
            return await _httpClient.GetJsonAsync<List<Activity>>("api/activities");
        }

        public Task Update(Activity activity)
        {
            throw new NotImplementedException();
        }
    }
}
