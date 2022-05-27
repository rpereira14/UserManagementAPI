using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
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
using UserManagementApi.Test.MockData;

namespace UserManagementApi.Test.Controllers
{
    public class TestCustomerController
    {
        #region GetAll
        //Test get all customers return 200
        [Fact]
        public async Task Get_ShouldReturnStatusCode200()
        {
            //Arrange
            var manager = new Mock<ICustomerManager>();
            var logger = new Mock<ILogger<CustomersController>>();
            manager.Setup(_ => _.GetAll()).ReturnsAsync(CustomerMockData.GetCustomers());
            var controller = new CustomersController(logger.Object, manager.Object);
            
            //Act
            var result = await controller.Get();

            //Assert
            (result.Result as OkObjectResult).StatusCode.Should().Be(200);
        }

        //Test get all customers return 204 if list is empty
        [Fact]
        public async Task Get_ShouldReturnStatusCode204()
        {
            //Arrange
            var manager = new Mock<ICustomerManager>();
            var logger = new Mock<ILogger<CustomersController>>();
            manager.Setup(_ => _.GetAll()).ReturnsAsync(CustomerMockData.EmptyCustomerList());
            var controller = new CustomersController(logger.Object, manager.Object);
            
            //Act
            var result = await controller.Get();

            //Assert
            (result.Result as NoContentResult).StatusCode.Should().Be(204);
        }

        //Test get all customers return same list as mocked
        [Fact]
        public async Task Get_ShouldReturnCustomerList()
        {
            //Arrange
            var manager = new Mock<ICustomerManager>();
            var logger = new Mock<ILogger<CustomersController>>();
            manager.Setup(_ => _.GetAll()).ReturnsAsync(CustomerMockData.GetCustomers());
            var controller = new CustomersController(logger.Object, manager.Object);
            
            var mockCustomers = CustomerMockData.GetCustomers();
            //Act
            var result = await controller.Get();

            //Assert
            (result.Result as OkObjectResult).Value.Should().NotBeNull();
            Assert.Equal(mockCustomers, (result.Result as OkObjectResult).Value);
        }
        #endregion

        #region GetCustomer
        //Test get customer returns the desired customer by id
        [Fact]
        public async Task Get_ShouldReturnOneCustomerByID()
        {
            //Arrange
            var manager = new Mock<ICustomerManager>();
            var logger = new Mock<ILogger<CustomersController>>();
            manager.Setup(_ => _.GetCustomer(1)).ReturnsAsync(CustomerMockData.GetCustomer(1));
            var controller = new CustomersController(logger.Object, manager.Object);
            
            var mockCustomer = CustomerMockData.GetCustomer(1);
            //Act
            var result = await controller.Get(1);

            //Assert
            (result.Result as OkObjectResult).Value.Should().NotBeNull();
            Assert.Equal(mockCustomer, (result.Result as OkObjectResult).Value);
        }

        [Fact]
        public async Task GetCustomer_ShouldReturnStatusCode200()
        {
            //Arrange
            var manager = new Mock<ICustomerManager>();
            var logger = new Mock<ILogger<CustomersController>>();
            manager.Setup(_ => _.GetCustomer(1)).ReturnsAsync(CustomerMockData.GetCustomer(1));
            var controller = new CustomersController(logger.Object, manager.Object);

            //Act
            var result = await controller.Get(1);

            //Assert
            (result.Result as OkObjectResult).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetCustomer_ShouldReturnStatusCode204()
        {
            //Arrange
            var manager = new Mock<ICustomerManager>();
            var logger = new Mock<ILogger<CustomersController>>();
            manager.Setup(_ => _.GetCustomer(0)).ReturnsAsync(CustomerMockData.GetCustomer(0));
            var controller = new CustomersController(logger.Object, manager.Object);

            //Act
            var result = await controller.Get(0);

            //Assert
            (result.Result as BadRequestObjectResult).StatusCode.Should().Be(400);
        }
        #endregion

        #region AddCustomer
        //Test get all customers return 200
        [Fact]
        public async Task AddCustomer_ShouldReturnStatusCode200()
        {
            //Arrange
            var manager = new Mock<ICustomerManager>();
            var logger = new Mock<ILogger<CustomersController>>();
            var newCustomer = new Customer { Email = "newcust@email.com", FirstName = "new", Surname = "cust", Password = Guid.NewGuid().ToString() };
            manager.Setup(_ => _.AddCustomer(newCustomer)).ReturnsAsync(CustomerMockData.AddCustomer(newCustomer));
            var controller = new CustomersController(logger.Object, manager.Object);

            //Act
            var result = await controller.AddCustomer(newCustomer);

            //Assert
            (result.Result as OkObjectResult).StatusCode.Should().Be(200);
        }


        //Test get all customers return same list as mocked
        [Fact]
        public async Task AddCustomer_ShouldReturnCustomerList()
        {
            //Arrange
            var manager = new Mock<ICustomerManager>();
            var logger = new Mock<ILogger<CustomersController>>();
            var newCustomer = new Customer { Email = "newcust@email.com", FirstName = "new", Surname = "cust", Password = Guid.NewGuid().ToString() };
            manager.Setup(_ => _.AddCustomer(newCustomer)).ReturnsAsync(CustomerMockData.AddCustomer(newCustomer));
            var controller = new CustomersController(logger.Object, manager.Object);

            //Act
            var result = await controller.AddCustomer(newCustomer);

            //Assert
            (result.Result as OkObjectResult).Value.Should().NotBeNull();
            Assert.Equal(CustomerMockData.CUSTOMERS, (result.Result as OkObjectResult).Value);
        }

        #endregion
    }
}
