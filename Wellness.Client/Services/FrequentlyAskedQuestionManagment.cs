using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Client.Services
{
    public class FrequentlyAskedQuestionManagment : IFrequentlyAskedQuestionService
    {
        public const string EndpointName = "frequentlyaskedquestions";
        private Lazy<Task<IEnumerable<PersistenceWrapper<FrequentlyAskedQuestion>>>> _events;

        private readonly HttpClient _httpClient;
        public FrequentlyAskedQuestionManagment(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("Default");
            Reset();
        }

        private void Reset()
        {
            _events = new Lazy<Task<IEnumerable<PersistenceWrapper<FrequentlyAskedQuestion>>>>(async () => await _httpClient.GetJsonAsync<List<PersistenceWrapper<FrequentlyAskedQuestion>>>($"api/{EndpointName}"));
        }

        public async Task Create(FrequentlyAskedQuestion eventObj, CancellationToken cancellationToken)
        {
            await _httpClient.PostJsonAsync($"api/{EndpointName}", eventObj);
            Reset();
        }
        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            await _httpClient.DeleteAsync($"api/{EndpointName}/{id}");
            Reset();
        }

        public async Task<IEnumerable<PersistenceWrapper<FrequentlyAskedQuestion>>> GetAll(CancellationToken cancellationToken)
        {
            return await _events.Value;
        }
        public async Task<PersistenceWrapper<FrequentlyAskedQuestion>> Get(Guid id, CancellationToken cancellationToken)
        {
            return (await _events.Value).FirstOrDefault(i => i.Model.Id == id);
        }

        public async Task Update(FrequentlyAskedQuestion eventObj, CancellationToken cancellationToken)
        {
            await _httpClient.PutJsonAsync($"api/{EndpointName}/{eventObj.Id}", eventObj);
            Reset();
        }

        public async Task<string> UploadFile(string name, string contentType, Func<Stream, Task> streamTask, CancellationToken cancellationToken)
        {
            using (var stream = new MemoryStream())
            {
                await streamTask(stream);
                using (var form = new MultipartFormDataContent())
                using (HttpContent fileContent = new ByteArrayContent(stream.GetBuffer()))
                {
                    form.Add(fileContent, "\"upload\"", name);
                    var result = await _httpClient.PostAsync($"api/wellnessfiles", form);
                    return (await result.Content.ReadFromJsonAsync<List<EventAttachment>>()).First().FilePath;
                }
            }
        }
    }
}
