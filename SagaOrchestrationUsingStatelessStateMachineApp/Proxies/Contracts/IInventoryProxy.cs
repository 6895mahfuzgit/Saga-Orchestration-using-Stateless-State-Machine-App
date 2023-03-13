using SagaOrchestrationUsingStatelessStateMachineApp.Models;

namespace SagaOrchestrationUsingStatelessStateMachineApp.Proxies.Contracts
{
    public interface IInventoryProxy
    {
        Task<(List<Inventory>,bool)> Add(List<Inventory> items);
        Task<(List<Inventory>,bool)> Remove(List<Inventory> items);

    }
}
