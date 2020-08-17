using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Client.Services
{
    public class ActivityParticipationManagement : IActivityParticipationService
    {

        private readonly HttpClient _httpClient;
        public ActivityParticipationManagement(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("Default");
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
