using SagaOrchestrationUsingStatelessStateMachineApp.Models;

namespace SagaOrchestrationUsingStatelessStateMachineApp
{
    public interface ITranspostProxy
    {
        Task<(int,bool)> Add(Transpost  transpost);
        Task<(int,bool)> Remove(Transpost  transpost);
    }
}
