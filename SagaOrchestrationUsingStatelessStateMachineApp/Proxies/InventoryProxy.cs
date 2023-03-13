using Newtonsoft.Json;
using SagaOrchestrationUsingStatelessStateMachineApp.Models;
using SagaOrchestrationUsingStatelessStateMachineApp.Proxies.Contracts;
using System.Text;

namespace SagaOrchestrationUsingStatelessStateMachineApp.Proxies
{
    public class InventoryProxy : IInventoryProxy
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public InventoryProxy(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<(List<Inventory>,bool)> Add(List<Inventory> items)
        {
            var request = JsonConvert.SerializeObject(items);
            var inventoryClient = _httpClientFactory.CreateClient("Inventory");
            var inventoryResponse = await inventoryClient.PostAsync("/api/Inventory/add", new StringContent(request, Encoding.UTF8, "application/JSON"));

            if (inventoryResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(inventoryResponse.ReasonPhrase);
            }

            List<Inventory> result = JsonConvert.DeserializeObject<List<Inventory>>(await inventoryResponse.Content.ReadAsStringAsync());
            return (result, true);
        }

        public async Task<(List<Inventory>, bool)> Remove(List<Inventory> items)
        {
            var request = JsonConvert.SerializeObject(items);
            var inventoryClient = _httpClientFactory.CreateClient("Inventory");
            var inventoryResponse = await inventoryClient.PostAsync("/api/Inventory/remove", new StringContent(request, Encoding.UTF8, "application/JSON"));

            if (inventoryResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(inventoryResponse.ReasonPhrase);
            }

            List<Inventory> result = JsonConvert.DeserializeObject<List<Inventory>>(await inventoryResponse.Content.ReadAsStringAsync());
            return (result, true);
        }
    }
}
