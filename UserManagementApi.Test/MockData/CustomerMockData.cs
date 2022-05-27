using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementApi.Models;

namespace UserManagementApi.Test.MockData
{
    public class CustomerMockData
    {
        public static List<Customer> CUSTOMERS = new List<Customer>
        {
         new Customer
                {
                    FirstName = "Rui",
                    Surname = "Pereira",
                    Email = "rui.pereira@email.com",
                    Password = Guid.NewGuid().ToString(),
                    Id = 1
                },
                new Customer
                {
                    FirstName = "Joao",
                    Surname = "Pereira",
                    Email = "Joao.pereira@email.com",
                    Password = Guid.NewGuid().ToString(),
                    Id = 2
                },
                new Customer
                {
                    FirstName = "Antonio",
                    Surname = "Teixeira",
                    Email = "Antonio.Teixeira@email.com",
                    Password = Guid.NewGuid().ToString(),
                    Id = 3
                }};

        public static List<Customer> GetCustomers()
        {
            return CUSTOMERS;
        }
        public static List<Customer> EmptyCustomerList()
        {
            return new List<Customer>();
        }

        public static Customer GetCustomer(int id)
        {
            return CUSTOMERS.FirstOrDefault(c => c.Id == id);
        }

        public static List<Customer> AddCustomer(Customer customer)
        {
            CUSTOMERS.Add(customer);
            return CUSTOMERS;
        }
    }
}
