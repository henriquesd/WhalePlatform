using WP.Core.DomainObjects;

namespace WP.Customer.API.Models
{
    public class Address : Entity
    {
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string PostalCode { get; private set; }
        public string City { get; private set; }
        public string Province { get; private set; }
        public Guid CustomerId{ get; private set; }

        // EF Relation
        public Customer Customer { get; protected set; }

        public Address(string street, string number, string complement, string postalCode, string city, string province, Guid customerId)
        {
            Street = street;
            Number = number;
            Complement = complement;
            PostalCode = postalCode;
            City = city;
            Province = province;
            CustomerId = customerId;
        }

        // EF Constructor
        protected Address() { }
    }
}