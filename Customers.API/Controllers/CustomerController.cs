using Customers.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Customers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerDbContext _context;
        public CustomerController(CustomerDbContext dbContext)
        {
            _context = dbContext;
        }
        /// <summary>
        /// GetsAll the Customers Details
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            return _context.Customers;
        }

        /// <summary>
        /// Gets Customer Information by ID 
        /// </summary>

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Customer>> GetById(int id)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(id);
                if (customer == null)
                {
                    return NotFound();
                }

                return customer;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Retrieving Data From The Database");
            }


        }

        /// <summary>
        /// Create Customer and Save into SQL Server Database
        /// </summary>
        ///  /// <remarks>
        /// Sample request:
        /// 
        ///  POST api/Customer
        ///  {
        ///     "customerID": 100,
        ///     "customerName": "Abhishek",
        ///     "mobileNumber": "12345678",
        ///     "email": "a@a.com"
        ///   }
        /// </remarks>
        /// <param name="customer"></param>  
        /// /// <returns>A newly created employee</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>  
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCustomer(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return Ok();
        }
        /// <summary>
        /// Update Customer details and Save into SQL DB
        /// </summary>
       
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return Ok();
        }
        /// <summary>
        /// Delete the Customer by ID 
        /// <summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(id);
                if (customer == null)
                {
                    return NotFound($"Employee with Id = {id} not found");
                }
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               $"Error deleting data for Customer with ID = {id}");

            }




        }
    }
}
