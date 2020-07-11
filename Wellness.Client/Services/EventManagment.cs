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
using System.Security.Cryptography;

namespace Wellness.Client.Services
{
    public class EventManagment : IEventManagementService
    {
        private Lazy<Task<IEnumerable<PersistenceWrapper<Event>>>> _events;

        private readonly HttpClient _httpClient;
        public EventManagment(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _events = new Lazy<Task<IEnumerable<PersistenceWrapper<Event>>>>(async () => await _httpClient.GetJsonAsync<List<PersistenceWrapper<Event>>>("api/events"));
        }

        public async Task Create(Event eventObj)
        {
            await _httpClient.PostJsonAsync($"api/events", eventObj);
        }
        public async Task Disable(Guid eventId)
        {
            await _httpClient.DeleteAsync($"api/events/{eventId}");
        }

        public async Task<IEnumerable<PersistenceWrapper<Event>>> GetAll()
        {
            return await _events.Value;
        }

        public async Task Update(Event eventObj)
        { 
            await _httpClient.PutJsonAsync($"api/events/{eventObj.Id}", eventObj);
        }

    }
}
