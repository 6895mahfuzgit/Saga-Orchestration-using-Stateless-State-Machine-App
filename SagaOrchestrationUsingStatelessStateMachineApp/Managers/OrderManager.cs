using SagaOrchestrationUsingStatelessStateMachineApp.Models;
using SagaOrchestrationUsingStatelessStateMachineApp.Proxies.Contracts;

namespace SagaOrchestrationUsingStatelessStateMachineApp.Managers
{
    public class OrderManager : IOrderManager
    {
        private readonly ITranspostProxy _transpostProxy;
        private readonly IOrderProxy _orderProxy;
        private readonly IInventoryProxy _inventoryProxy;

        enum OrderTransactionState
        {
            NotStarted,
            OrderCreated,
            OrderCancelled,
            OrderCreatedFailed,
            TransportCreated,
            TransportCancelled,
            TransportCreatedFailed,
            InventoryUpdated,
            InventoryUpdateFailed,
            InventoryRollback,
            InventoryCancel
        }

        enum OrderAction
        {
            CreateOrder,
            CancelOrder,
            UpdateInventory,
            RollbackInventory,
            CancelInventory,
            CreateTransport,
            RollbackTransport,
        }

        public OrderManager(ITranspostProxy transpostProxy, IOrderProxy orderProxy, IInventoryProxy inventoryProxy)
        {
            _transpostProxy = transpostProxy;
            _orderProxy = orderProxy;
            _inventoryProxy = inventoryProxy;
        }

        public async Task<bool> CreateOrder(Order input)
        {
            Stateless.StateMachine<OrderTransactionState, OrderAction> orderStateMachine = new Stateless.StateMachine<OrderTransactionState, OrderAction>(OrderTransactionState.NotStarted);

            List<Inventory> items = new List<Inventory>();
            Transpost transpost = new();
            orderStateMachine.Configure(OrderTransactionState.NotStarted)
                             .PermitDynamic(OrderAction.CreateOrder, () =>
                             {
                                 var (order, isSuccess) = _orderProxy.Save(input).Result;
                                 input.Id = order.Id;
                                 return isSuccess ? OrderTransactionState.OrderCreated : OrderTransactionState.OrderCreatedFailed;
                             });

            orderStateMachine.Configure(OrderTransactionState.OrderCreated)
                             .PermitDynamic(OrderAction.UpdateInventory, () =>
                             {
                                 items = input.orderDtls.ToList().Select(x => new Inventory
                                 {
                                     ProductId = x.ProductId,
                                     ProductName = x.ProductName,
                                     Qty = x.Qty,
                                 }).ToList();

                                 var (inventoryItems, isSuccess) = _inventoryProxy.Remove(items).Result;
                                 return isSuccess ? OrderTransactionState.InventoryUpdated : OrderTransactionState.InventoryUpdateFailed;
                             }).OnEntry(() => orderStateMachine.Fire(OrderAction.UpdateInventory));

            orderStateMachine.Configure(OrderTransactionState.InventoryUpdated)
                             .PermitDynamic(OrderAction.CreateTransport, () =>
                             {
                                 transpost = new Transpost
                                 {
                                     OrderId = input.Id,
                                     OrderRefNo = input.RefNo,
                                     Status = "N"
                                 };

                                 var (inventoryItem, isSuccess) = _transpostProxy.Add(transpost).Result;
                                 return isSuccess ? OrderTransactionState.TransportCreated : OrderTransactionState.TransportCreatedFailed;
                             }).OnEntry(() => orderStateMachine.Fire(OrderAction.CreateTransport));

            orderStateMachine.Configure(OrderTransactionState.InventoryUpdateFailed)
                              .PermitDynamic(OrderAction.RollbackInventory, () =>
                              {
                                  _inventoryProxy.Add(items);
                                  return OrderTransactionState.InventoryRollback;
                              }).OnEntry(() => orderStateMachine.Fire(OrderAction.RollbackInventory));

            orderStateMachine.Configure(OrderTransactionState.TransportCreatedFailed)
                 .PermitDynamic(OrderAction.CancelInventory, () =>
                 {
                     _inventoryProxy.Add(items);
                     return OrderTransactionState.InventoryRollback;
                 }).OnEntry(() => orderStateMachine.Fire(OrderAction.CancelInventory));

            orderStateMachine.Configure(OrderTransactionState.InventoryRollback)
                              .PermitDynamic(OrderAction.CancelOrder, () =>
                              {
                                  _orderProxy.Delete(input.Id);
                                  return OrderTransactionState.OrderCancelled;
                              }).OnEntry(() => orderStateMachine.Fire(OrderAction.CancelOrder));

            orderStateMachine.Fire(OrderAction.CreateOrder);

            return orderStateMachine.State == OrderTransactionState.TransportCreated;
        }
    }
}
