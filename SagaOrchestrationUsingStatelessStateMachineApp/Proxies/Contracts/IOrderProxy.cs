using SagaOrchestrationUsingStatelessStateMachineApp.Models;

namespace SagaOrchestrationUsingStatelessStateMachineApp.Proxies.Contracts
{
    public interface IOrderProxy
    {
        Task<(Order, bool)> Save(Order order);
        Task<(Order, bool)> Delete(int id);
    }
}
