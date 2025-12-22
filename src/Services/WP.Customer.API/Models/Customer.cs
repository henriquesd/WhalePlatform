using WP.Core.DomainObjects;

namespace WP.Customer.API.Models
{
    public class Customer : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public Email Email { get; private set; }
        public bool Deleted { get; private set; }
        public Address Address { get; private set; }

        // EF Relation
        protected Customer() { }

        public Customer(Guid id, string name, string email)
        {
            Id = id;
            Name = name ;
            Email = new Email(email);
            Deleted = false;
        }

        public void UpdateEmail(string email)
        {
            Email = new Email(email);
        }

        public void UpdateAddress(Address address)
        {
            Address = address;
        }
    }
}