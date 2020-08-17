using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Client.Services
{
    public class EventManagment : IEventManagementService
    {
        private Lazy<Task<IEnumerable<PersistenceWrapper<Event>>>> _events;

        private readonly HttpClient _httpClient;
        public EventManagment(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("Default");
            Reset();
        }

        private void Reset()
        {
            _events = new Lazy<Task<IEnumerable<PersistenceWrapper<Event>>>>(async () => await _httpClient.GetJsonAsync<List<PersistenceWrapper<Event>>>("api/events"));
        }

        public async Task Create(Event eventObj)
        {
            await _httpClient.PostJsonAsync($"api/events", eventObj);
            Reset();
        }
        public async Task Disable(Guid eventId)
        {
            await _httpClient.DeleteAsync($"api/events/{eventId}");
            Reset();
        }

        public async Task<IEnumerable<PersistenceWrapper<Event>>> GetAll(CancellationToken cancellationToken)
        {
            return await _events.Value;
        }
        public async Task<PersistenceWrapper<Event>> Get(Guid id, CancellationToken cancellationToken)
        {
            return (await _events.Value).FirstOrDefault(i => i.Model.Id == id);
        }

        public async Task Update(Event eventObj)
        {
            await _httpClient.PutJsonAsync($"api/events/{eventObj.Id}", eventObj);
            Reset();
        }

    }
}
