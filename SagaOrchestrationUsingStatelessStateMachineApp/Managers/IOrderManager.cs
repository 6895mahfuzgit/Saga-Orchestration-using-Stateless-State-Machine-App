using SagaOrchestrationUsingStatelessStateMachineApp.Models;

namespace SagaOrchestrationUsingStatelessStateMachineApp.Managers
{
    public interface IOrderManager
    {
        Task<bool> CreateOrder(Order order);
    }
}
