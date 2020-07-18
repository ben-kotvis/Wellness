using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Client.Services
{
    public class EventParticipationManagement : IEventParticipationService
    {

        private readonly HttpClient _httpClient;
        public EventParticipationManagement(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("Default");
        }

        public async Task Create(EventParticipation eventParticipation)
        {
            await _httpClient.PostJsonAsync($"api/eventParticipations", eventParticipation);
        }

        public async Task Delete(Guid id)
        {
            await _httpClient.DeleteAsync($"api/eventParticipations/{id}");
        }

        public async Task<IEnumerable<PersistenceWrapper<EventParticipation>>> GetByRelativeMonthIndex(int relativeMonthIndex, Guid userId)
        {
            return await _httpClient.GetJsonAsync<List<PersistenceWrapper<EventParticipation>>>($"api/eventParticipations/users/{userId}/relativeIndex/{relativeMonthIndex}");
        }

        public async Task<string> UploadFile(string name, string contentType, Func<Stream, Task> streamTask)
        {
            throw new NotImplementedException();
        }
    }
}
