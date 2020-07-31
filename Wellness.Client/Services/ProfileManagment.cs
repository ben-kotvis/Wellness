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
    public class ProfileManagment : IProfileService
    {
        private readonly HttpClient _httpClient;
        public ProfileManagment(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("Default");
        }

        public async Task Create(User user)
        {
            await _httpClient.PostJsonAsync($"api/profiles", user);
        }
        public async Task Disable(Guid eventId)
        {
            await _httpClient.DeleteAsync($"api/profiles/{eventId}");
        }

        public async Task<IEnumerable<PersistenceWrapper<User>>> Find(string searchText, CancellationToken cancellationToken)
        {
            return await _httpClient.GetJsonAsync<List<PersistenceWrapper<User>>>("api/profiles");
        }
        public async Task<PersistenceWrapper<User>> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _httpClient.GetJsonAsync<PersistenceWrapper<User>>($"api/profiles/{id}");
        }

        public async Task<PersistenceWrapper<User>> GetCurrent(CancellationToken cancellationToken)
        {
            try
            {
                return await _httpClient.GetJsonAsync<PersistenceWrapper<User>>("api/profiles/me");
            }
            catch (Exception)
            {

            }
            return default;
        }

        public async Task Update(User user)
        {
            await _httpClient.PutJsonAsync($"api/profiles/{user.Id}", user);
        }

    }
}
