namespace SagaOrchestrationUsingStatelessStateMachineApp.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }

    }
}
