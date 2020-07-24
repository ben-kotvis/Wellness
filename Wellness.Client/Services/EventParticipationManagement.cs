using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Wellness.Model;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading;

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
            using (var stream = new MemoryStream())
            {
                await streamTask(stream);
                using (var form = new MultipartFormDataContent())
                using (HttpContent fileContent = new ByteArrayContent(stream.GetBuffer()))
                {   
                    form.Add(fileContent, "\"upload\"", name);
                    fileContent.Headers.Add("Content-Type", "application/octet-stream");
                    var result = await _httpClient.PostAsync($"api/wellnessfiles", form);
                    return (await result.Content.ReadFromJsonAsync<List<EventAttachment>>()).First().FilePath;
                }
            }
        }

        public async Task<byte[]> DownloadFile(string path)
        {
            return await _httpClient.GetByteArrayAsync(path);
        }
    }
}
