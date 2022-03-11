using Microsoft.AspNetCore.Mvc;
using Postdb.DTOs;
using Postdb.Models;
using Postdb.Repositories;

namespace Postdb.Controllers;

[ApiController]
[Route("api/customer")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly ICustomerRepository _Customer;
    private readonly IOrderRepository _order;

    public CustomerController(ILogger<CustomerController> logger, ICustomerRepository Customer,IOrderRepository order)
    {
        _logger = logger;
        _Customer = Customer;
        _order = order;
    }
    [HttpGet]
    public async Task<ActionResult<List<CustomerDTO>>> GetAllCustomers()
    {
        var CustomerList = await _Customer.GetList();

        var dtoList = CustomerList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{customer_id}")]

    public async Task<ActionResult<CustomerDTO>> GetById([FromRoute] long customer_id)
    {
        var Customer = await _Customer.GetById(customer_id);
        if (Customer is null)
            return NotFound("No Customer found with given employee number");
            var asDto = Customer.asDto;
                asDto.MyOrders = await _order.GetList(customer_id);
            // OrderDTO.Orders =await _order.GetList(order_id);
        return Ok(asDto);
    }

    [HttpPost]

    public async Task<ActionResult<CustomerDTO>> CreateCustomer([FromBody] CreateCustomerDTO Data)
    {
        if (!(new string[] { "male", "female" }.Contains(Data.Gender.Trim().ToLower())))
            return BadRequest("Gender value is not recognized");

        // var subtractDate = DateTimeOffset.Now - Data.DateOfBirth;
        // if (subtractDate.TotalDays / 365 < 18.0)
        // return BadRequest("Employee must be at least 18 years old");/

        var toCreateCustomer = new Customer
        {
            FirstName = Data.FirstName.Trim(),
            LastName = Data.LastName.Trim(),
            // DateOfBirth = Data.DateOfBirth.UtcDateTime,
            Email = Data.Email.Trim().ToLower(),
            Gender = Enum.Parse<Gender>(Data.Gender, true),
            Mobile = Data.Mobile,


        };
        var createdCustomer = await _Customer.Create(toCreateCustomer);

        return StatusCode(StatusCodes.Status201Created, createdCustomer.asDto);


    }

    [HttpPut("{customer_id}")]
    public async Task<ActionResult> UpdateCustomer([FromRoute] long CustomerId,
    [FromBody] CustomerUpdateDTO Data)
    {
        var existing = await _Customer.GetById(CustomerId);
        if (existing is null)
            return NotFound("No Customer found with given employee number");

        var toUpdateCustomer = existing with
        {
            Email = Data.Email?.Trim()?.ToLower() ?? existing.Email,
            // LastName = Data.LastName?.Trim() ?? existing.LastName,
            Mobile = Data.Mobile ?? existing.Mobile,
            // DateOfBirth = existing.DateOfBirth.UtcDateTime,
        };

        var didUpdate = await _Customer.Update(toUpdateCustomer);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update");
        return NoContent();
    }

    [HttpDelete("{customer_id}")]
    public async Task<ActionResult> DeleteCustomer([FromRoute] long CustomerId)
    {
        var existing = await _Customer.GetById(CustomerId);
        if (existing is null)
            return NotFound("No Customer found with given employee number");
        await _Customer.Delete(CustomerId);
        return NoContent();
    }



}
