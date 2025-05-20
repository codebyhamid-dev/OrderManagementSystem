using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.DTOs;
using OrderManagementSystem.Repository.CustomerRepo;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _repository;

        public CustomersController(ICustomerRepository repository)
        {
            _repository = repository;
        }
        [HttpPost("AddCustomers")]
        public async Task<IActionResult> Create([FromBody] CustomerDTO customerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customer=await _repository.CreateCustomerAsync(customerDTO);
            if (customer == null) {
                return BadRequest("No Customer found");
            }
            return Ok(customer);
        }

        [HttpGet("AllCustomers")]
        public async Task<IActionResult> GetAll()
        {
            var customer=await _repository.GetAllCustomersAsync();
            if (customer == null) {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpGet("GetCustomers/By/{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            if (Id == 0)
            {
                return NotFound();
            }
            var customer = await _repository.GetCustomerByIdAsync(Id);
            if (customer == null) {
                return BadRequest("No Customer Found");
            }
            return Ok(customer);
        }

        [HttpPut("UpdateCustomer/Id/{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] CustomerDTO customerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _repository.UpdateCustomerAsync(Id, customerDTO);
            if (result == null) {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("DeleteCustomer/Id/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            if (Id == 0) { 
                return NotFound();
            }
            var customer=await _repository.DeleteCustomerAsync(Id);
            if (customer == null) {
                return BadRequest("Customer Not found");
            }
            return Ok(customer);
        }
        [HttpGet("Count")]
        public async Task<IActionResult> GetCustomerCount()
        {
            var count = await _repository.GetCustomerCountAsync();
            return Ok($"Total Number of Customers are: {count}");

        }
    }
} 
