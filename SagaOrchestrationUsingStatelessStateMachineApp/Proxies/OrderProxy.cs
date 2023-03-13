using Newtonsoft.Json;
using SagaOrchestrationUsingStatelessStateMachineApp.Models;
using SagaOrchestrationUsingStatelessStateMachineApp.Proxies.Contracts;
using System.Text;

namespace SagaOrchestrationUsingStatelessStateMachineApp.Proxies
{
    public class OrderProxy : IOrderProxy
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OrderProxy(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<(Order, bool)> Save(Order order)
        {
            var request = JsonConvert.SerializeObject(order);
            var orderClient = _httpClientFactory.CreateClient("Order");
            var orderResponse = await orderClient.PostAsync("/api/Order", new StringContent(request, Encoding.UTF8, "application/JSON"));

            if (orderResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(orderResponse.ReasonPhrase);
            }

            Order result = JsonConvert.DeserializeObject<Order>(await orderResponse.Content.ReadAsStringAsync());
            return (result, true);
        }

        public async Task<(Order, bool)> Delete(int id)
        {
            //var request = JsonConvert.SerializeObject(id);
            var orderClient = _httpClientFactory.CreateClient("Order");
            var orderResponse = await orderClient.DeleteAsync($"/api/Order/{id}");

            if (orderResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(orderResponse.ReasonPhrase);
            }

            Order result = JsonConvert.DeserializeObject<Order>(await orderResponse.Content.ReadAsStringAsync());
            return (result, true);
        }
    }
}
