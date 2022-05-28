using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementApi.Controllers;
using UserManagementApi.Managers;
using UserManagementApi.Models;
using UserManagementApi.Repositories;
using UserManagementApi.Test.MockData;

namespace UserManagementApi.Test.Repositories
{
    public class TestCustomerRepository : IDisposable
    {

        private readonly DataContext _dbContext;

        public TestCustomerRepository()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new DataContext(options);
        }

        #region Content test

        //Test getAll method from CustomerRepository
        [Fact]
        public async Task GetAll_ShouldReturnCustomerColletion()
        {
            // Arrange
            if (_dbContext.Customers.Count() == 0)
            {
                _dbContext.Customers.AddRange(CustomerMockData.GetCustomersCopy());
                await _dbContext.SaveChangesAsync();
            }

            var repository = new CustomerRepository(_dbContext);

            // Act
            var result = await repository.GetAll();

            // Assert
            Assert.Equal(_dbContext.Customers.ToList(), result);
        }

        //Test GetCustomer method from CustomerRepository
        [Fact]
        public async Task GetCustomer_ShouldReturnCorrectCustomer()
        {
            // Arrange
            if (_dbContext.Customers.Count() == 0)
            {
                _dbContext.Customers.AddRange(CustomerMockData.GetCustomersCopy());
                await _dbContext.SaveChangesAsync();
            }

            var repository = new CustomerRepository(_dbContext);

            // Act
            var result = await repository.GetCustomer(1);

            // Assert
            Assert.Equal(_dbContext.Customers.Find(1), result);
        }

        //Test AddCustomer method from CustomerRepository
        [Fact]
        public async Task AddCustomer_ShouldAddCustomer()
        {
            // Arrange
            if (_dbContext.Customers.Count() == 0)
            {
                _dbContext.Customers.AddRange(CustomerMockData.GetCustomersCopy());
                await _dbContext.SaveChangesAsync();
            }

            var repository = new CustomerRepository(_dbContext);
            var newCust = new Customer
            {
                FirstName = "Diogo",
                Surname = "Jota",
                Email = "diogo.jota@email.com",
                Password = Guid.NewGuid().ToString(),
            };

            // Act
            var result = await repository.AddCustomer(newCust);

            // Assert
            result.Count().Should().Be(CustomerMockData.GetCustomersCopy().Count + 1);
            Assert.Equal(_dbContext.Customers, result);
        }
        
        //Test UpdateCustomer method from CustomerRepository
        [Fact]
        public async Task UpdateCustomer_ShouldAddCustomer()
        {
            // Arrange
            if (_dbContext.Customers.Count() == 0)
            {
                _dbContext.Customers.AddRange(CustomerMockData.GetCustomersCopy());
                await _dbContext.SaveChangesAsync();
            }

            var repository = new CustomerRepository(_dbContext);
            var updateCust = new Customer
            {
                Id = 1,
                FirstName = "Diogo2",
                Surname = "Jota2",
                Email = "diogo.jota@email.com",
                Password = Guid.NewGuid().ToString(),
            };

            // Act
            var result = await repository.UpdateCustomer(updateCust);

            // Assert
            result.Count().Should().Be(CustomerMockData.GetCustomersCopy().Count);
            Assert.Equal(_dbContext.Customers.Find(1), result.ToList().Find(c=>c.Id == 1));
        }


        #endregion

        #region CalledOnce Test

        //Test getall is called
        [Fact]
        public async Task GetAll_ShouldCallAddCustomerOnce()
        {
            // Arrange
            var repository = new Mock<ICustomerRepository>();
            var manager = new CustomerManager(repository.Object);
            var logger = new Mock<ILogger<CustomersController>>();
           
            var controller = new CustomersController(logger.Object,manager);

            // Act
            var result = controller.Get();

            // Assert
            repository.Verify(_ => _.GetAll(), Times.Exactly(1));
        }

         //Test getCustomer is called
        [Fact]
        public async Task GetCustomer_ShouldCallAddCustomerOnce()
        {
            // Arrange
            var repository = new Mock<ICustomerRepository>();
            var manager = new CustomerManager(repository.Object);
            var logger = new Mock<ILogger<CustomersController>>();
            
            var controller = new CustomersController(logger.Object,manager);

            // Act
            var result = controller.Get(1);

            // Assert
            repository.Verify(_ => _.GetCustomer(1), Times.Exactly(1));
        }

         //Test addCustomer is called
        [Fact]
        public async Task Add_ShouldCallAddCustomerOnce()
        {
            // Arrange
            var repository = new Mock<ICustomerRepository>();
            var manager = new CustomerManager(repository.Object);
            var logger = new Mock<ILogger<CustomersController>>();
            var newCust = new Customer
            {
                FirstName = "Diogo",
                Surname = "Jota",
                Email = "diogo.jota@email.com",
                Password = Guid.NewGuid().ToString(),
            };
            var controller = new CustomersController(logger.Object,manager);

            // Act
            var result = controller.AddCustomer(newCust);

            // Assert
            repository.Verify(_ => _.AddCustomer(newCust), Times.Exactly(1));
        }

        [Fact]
        public async Task Update_ShouldCallUpdateCustomerOnde()
        {
            // Arrange
            var repository = new Mock<ICustomerRepository>();
            var manager = new CustomerManager(repository.Object);
            var logger = new Mock<ILogger<CustomersController>>();
            var newCust = new Customer
            {
                Id = 1,
                FirstName = "Diogo",
                Surname = "Jota",
                Email = "diogo.jota@email.com",
                Password = Guid.NewGuid().ToString(),
            };
            var controller = new CustomersController(logger.Object, manager);

            // Act
            var result = controller.UpdateCustomer(newCust);

            // Assert
            repository.Verify(_ => _.UpdateCustomer(newCust), Times.Exactly(1));
        }

        #endregion


        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}
