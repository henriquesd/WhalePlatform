using MediatR;

namespace WP.Customer.API.Application.Events
{
    public class CustomerEventHandler : INotificationHandler<CustomerRegisteredEvent>
    {
        public Task Handle(CustomerRegisteredEvent notification, CancellationToken cancellationToken)
        {
            // Send notification event
            return Task.CompletedTask;
        }
    }
}