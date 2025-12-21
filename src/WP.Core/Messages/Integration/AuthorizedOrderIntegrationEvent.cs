namespace WP.Core.Messages.Integration
{
    public class AuthorizedOrderIntegrationEvent : IntegrationEvent
    {
        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
        public IDictionary<Guid, int> Items { get; private set; }

        public AuthorizedOrderIntegrationEvent(Guid clientId, Guid orderId, IDictionary<Guid, int> items)
        {
            ClientId = clientId;
            OrderId = orderId;
            Items = items;
        }
    }
}