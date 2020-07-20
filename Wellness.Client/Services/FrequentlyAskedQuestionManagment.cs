using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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

        public Task<string> UploadFile(string name, string contentType, Func<Stream, Task> streamTask, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
