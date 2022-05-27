using UserManagementApi.Models;

namespace UserManagementApi.Repositories

{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetCustomer(int id);
        Task<IEnumerable<Customer>> AddCustomer(Customer customer);
        Task<IEnumerable<Customer>> UpdateCustomer(Customer customer);
        Task<Customer> DeleteCustomer(int id);
    }
}
