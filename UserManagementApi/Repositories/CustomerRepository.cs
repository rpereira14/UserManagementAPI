using UserManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace UserManagementApi.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _context;

        public CustomerRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return null;
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return null;
            return customer;
        }

        public async Task<IEnumerable<Customer>> UpdateCustomer(Customer customer)
        {
            var existingCustomer = await _context.Customers.FindAsync(customer.Id);
            if (existingCustomer == null)
                return null;

            existingCustomer.Surname = customer.Surname;
            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.Email = customer.Email;
            existingCustomer.Password = customer.Password;

            await _context.SaveChangesAsync();
            return await _context.Customers.ToListAsync();
        }
    }
}
