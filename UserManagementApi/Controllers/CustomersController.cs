using Microsoft.AspNetCore.Mvc;
using UserManagementApi.Managers;
using UserManagementApi.Models;

namespace UserManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {

        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomerManager _customerManager;

        public CustomersController(ILogger<CustomersController> logger, ICustomerManager customerManager)
        {
            _customerManager = customerManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            var customers = await _customerManager.GetAll();
            if(customers == null || customers.Count() == 0)
                return NoContent();
            return Ok(customers);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Customer>>> Get(int id)
        {
            var customer = await _customerManager.GetCustomer(id);
            if (customer == null) 
                return BadRequest("Customer not found.");
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Customer>>> AddCustomer([FromBody]Customer customer)
        {
            var addedCustomer = await _customerManager.AddCustomer(customer);
            return Ok(addedCustomer);
        }

        [HttpPut]
        public async Task<ActionResult<IEnumerable<Customer>>> UpdateCustomer([FromBody] Customer customer)
        {
            var updatedCustomer = await _customerManager.UpdateCustomer(customer);
            if (updatedCustomer == null)
                return BadRequest("Customer not found.");
            return Ok(updatedCustomer);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Customer>>> Delete(int id)
        {
            var customer = await _customerManager.DeleteCustomer(id);
            if (customer == null)
                return BadRequest("Customer not found.");
            return Ok(customer);
        }

    }
}