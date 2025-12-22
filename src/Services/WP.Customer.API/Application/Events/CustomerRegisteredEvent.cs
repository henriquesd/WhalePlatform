using WP.Core.Messages;

namespace WP.Customer.API.Application.Events
{
     public class CustomerRegisteredEvent : Event
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        public CustomerRegisteredEvent(Guid id, string name, string email)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
        }
    }
}