using Newtonsoft.Json;
using SagaOrchestrationUsingStatelessStateMachineApp.Models;
using SagaOrchestrationUsingStatelessStateMachineApp.Proxies.Contracts;
using System.Text;

namespace SagaOrchestrationUsingStatelessStateMachineApp.Proxies
{
    public class TranspostProxy : ITranspostProxy
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TranspostProxy(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<(Transpost, bool)> Add(Transpost transpost)
        {
            var request = JsonConvert.SerializeObject(transpost);
            var transpostClient = _httpClientFactory.CreateClient("Transport");
            var transpostResponse = await transpostClient.PostAsync("/api/Transpost/add", new StringContent(request, Encoding.UTF8, "application/JSON"));

            if (transpostResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(transpostResponse.ReasonPhrase);
            }

            var transportFromResponse = JsonConvert.DeserializeObject<Transpost>(await transpostResponse.Content.ReadAsStringAsync());
            return (transportFromResponse, true);
        }

        public async Task<(Transpost, bool)> Remove(Transpost transpost)
        {
            var request = JsonConvert.SerializeObject(transpost);
            var transpostClient = _httpClientFactory.CreateClient("Transport");
            var transpostResponse = await transpostClient.PostAsync("/api/Transpost/remove", new StringContent(request, Encoding.UTF8, "application/JSON"));

            if (transpostResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(transpostResponse.ReasonPhrase);
            }

            var transportFromResponse = JsonConvert.DeserializeObject<Transpost>(await transpostResponse.Content.ReadAsStringAsync());
            return (transportFromResponse, true);
        }
    }


}
