using WP.Core.Data;
using WP.Customer.API.Models;

namespace WP.Customer.API.Repositories
{
    public interface ICustomerRepository : IRepository<Models.Customer>
    {
        void Add(Models.Customer Customer);

        Task<IEnumerable<Models.Customer>> GetAll();
        Task<Models.Customer> GetByEmail(string email);

        void AddAddress(Address address);
        Task<Address> GetAddressById(Guid id);
    }
}