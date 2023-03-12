﻿using Newtonsoft.Json;
using SagaOrchestrationUsingStatelessStateMachineApp.Models;
using System.Text;

namespace SagaOrchestrationUsingStatelessStateMachineApp
{
    public class TranspostProxy : ITranspostProxy
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TranspostProxy(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<(int, bool)> Add(Transpost transpost)
        {
            var request = JsonConvert.SerializeObject(transpost);
            var transpostClient = _httpClientFactory.CreateClient("Transport");
            var transpostResponse = await transpostClient.PostAsync("/api/Transpost/add", new StringContent(request, Encoding.UTF8, "application/JSON"));

            if (transpostResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(transpostResponse.ReasonPhrase);
            }

            var transportId = await transpostResponse.Content.ReadAsStringAsync();
            return (Convert.ToInt32(transportId), true);
        }

        public async Task<(int, bool)> Remove(Transpost transpost)
        {
            var request = JsonConvert.SerializeObject(transpost);
            var transpostClient = _httpClientFactory.CreateClient(request);
            var transpostResponse = await transpostClient.PostAsync("/api/Transpost/remove", new StringContent(request, Encoding.UTF8, "application/JSON"));

            if (transpostResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(transpostResponse.ReasonPhrase);
            }

            var transportId = await transpostResponse.Content.ReadAsStringAsync();
            return (Convert.ToInt32(transportId), true);
        }
    }
}
