using Microsoft.EntityFrameworkCore;
using WP.Core.Data;
using WP.Customer.API.Data;
using WP.Customer.API.Models;

namespace WP.Customer.API.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomersContext _context;

        public CustomerRepository(CustomersContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(Models.Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public async Task<IEnumerable<Models.Customer>> GetAll()
        {
            return await _context.Customers.AsNoTracking().ToListAsync();
        }

        public async Task<Models.Customer> GetByEmail(string email)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.Email.EmailAddress == email);
        }

        public void AddAddress(Address address)
        {
            _context.Addresses.Add(address);
        }

        public async Task<Address> GetAddressById(Guid id)
        {
            return await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
