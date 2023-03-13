using SagaOrchestrationUsingStatelessStateMachineApp.Models;

namespace SagaOrchestrationUsingStatelessStateMachineApp.Proxies.Contracts
{
    public interface ITranspostProxy
    {
        Task<(Transpost, bool)> Add(Transpost transpost);
        Task<(Transpost, bool)> Remove(Transpost transpost);
    }
}
