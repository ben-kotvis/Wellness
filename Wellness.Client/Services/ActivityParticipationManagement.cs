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
    public class ActivityParticipationManagement : IActivityParticipationService
    {

        private readonly HttpClient _httpClient;
        public ActivityParticipationManagement(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Create(ActivityParticipation activityParticipation)
        {
            await _httpClient.PostJsonAsync($"api/activityParticipations", activityParticipation);
        }

        public async Task Delete(Guid id)
        {
            await _httpClient.DeleteAsync($"api/activityParticipations/{id}");
        }

        public async Task<IEnumerable<PersistenceWrapper<ActivityParticipation>>> GetByRelativeMonthIndex(int relativeMonthIndex, Guid userId)
        {
            return await _httpClient.GetJsonAsync<List<PersistenceWrapper<ActivityParticipation>>>($"api/activityParticipations/users/{userId}/relativeIndex/{relativeMonthIndex}");
        }
    }
}
