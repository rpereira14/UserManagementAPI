using UserManagementApi.Models;

namespace UserManagementApi.Managers
{
    public interface ICustomerManager
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetCustomer(int id);
        Task<IEnumerable<Customer>> AddCustomer(Customer customer);
        Task<IEnumerable<Customer>> UpdateCustomer(Customer customer);
        Task<Customer> DeleteCustomer(int id);
    }
}
