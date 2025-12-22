namespace WP.Core.Messages.Integration
{
    public class OrderCancelledIntegrationEvent : IntegrationEvent
    {
        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }

        public OrderCancelledIntegrationEvent(Guid clientId, Guid orderId)
        {
            ClientId = clientId;
            OrderId = orderId;
        }
    }
}