namespace SagaOrchestrationUsingStatelessStateMachineApp.Models
{
    public class OrderDtl
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int Qty { get; set; }
        public int Price { get; set; }
    }
}
