﻿using System;
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
using System.Threading;

namespace Wellness.Client.Services
{
    public class ActivityManagment : IActivityManagementService
    {
        private readonly HttpClient _httpClient;

        private Lazy<Task<IEnumerable<PersistenceWrapper<Activity>>>> _activities;
        public ActivityManagment(HttpClient httpClient)
        {
            _httpClient = httpClient;
            Reset();
        }
        private void Reset()
        {
            _activities = new Lazy<Task<IEnumerable<PersistenceWrapper<Activity>>>>(async () => await _httpClient.GetJsonAsync<List<PersistenceWrapper<Activity>>>("api/activities"));
        }

        public async Task Create(Activity activity)
        {
            await _httpClient.PostJsonAsync($"api/activities", activity);
        }
        public async Task Disable(Guid activityId)
        {
            await _httpClient.DeleteAsync($"api/activities/{activityId}");
        }

        public async Task<PersistenceWrapper<Activity>> Get(Guid id, CancellationToken cancellationToken)
        {
            return (await _activities.Value).FirstOrDefault(i => i.Model.Id == id);
        }
        public async Task<IEnumerable<PersistenceWrapper<Activity>>> GetAll(CancellationToken cancellationToken)
        {
            return await _httpClient.GetJsonAsync<List<PersistenceWrapper<Activity>>>("api/activities");
        }

        public async Task Update(Activity activity)
        { 
            await _httpClient.PutJsonAsync($"api/activities/{activity.Id}", activity);
        }
    }
}