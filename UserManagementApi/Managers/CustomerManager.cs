using UserManagementApi.Models;
using UserManagementApi.Repositories;

namespace UserManagementApi.Managers
{
    public class CustomerManager : ICustomerManager
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerManager(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> AddCustomer(Customer customer)
        {
            return await _customerRepository.AddCustomer(customer);
        }

        public async Task<Customer> DeleteCustomer(int id)
        {
            return await _customerRepository.DeleteCustomer(id);
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            try
            {
                return await _customerRepository.GetAll();
            }
            catch(Exception Ex)
            {
                throw new Exception("");
            }
        }

        public async Task<Customer> GetCustomer(int id)
        {
            return await _customerRepository.GetCustomer(id);
        }

        public async Task<IEnumerable<Customer>> UpdateCustomer(Customer customer)
        {
            return await _customerRepository.UpdateCustomer(customer);
        }
    }
}
