namespace SagaOrchestrationUsingStatelessStateMachineApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string RefNo { get; set; }
        public ICollection<OrderDtl> orderDtls { get; set; }


        public Order()
        {
            orderDtls = new List<OrderDtl>();
        }

    }
}
