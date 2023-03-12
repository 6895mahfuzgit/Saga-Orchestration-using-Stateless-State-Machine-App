using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SagaOrchestrationUsingStatelessStateMachineApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly ITranspostProxy _transpostProxy;

        public ValuesController(ITranspostProxy transpostProxy)
        {
            _transpostProxy = transpostProxy;
        }

        [HttpGet]
        public async Task<bool> Request()
        {

            await _transpostProxy.Add(new Models.Transpost
            {
                OrderId = 3,
                OrderRefNo = "333",
                Status = "a"

            });

            return true;
        }
    }
}
