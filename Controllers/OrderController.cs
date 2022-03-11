using Microsoft.AspNetCore.Mvc;
using Postdb.DTOs;
using Postdb.Models;
using Postdb.Repositories;

namespace Postdb.Controllers;

[ApiController]
[Route("api/order")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly IOrderRepository _Order;
    private readonly IProductRepository _product;

private readonly ICustomerRepository _customer;

    public OrderController(ILogger<OrderController> logger, IOrderRepository Order,IProductRepository product,ICustomerRepository customer)
    {
        _logger = logger;
        _Order = Order;
        _product=product;
        _customer=customer;
    }
    [HttpGet]
    public async Task<ActionResult<List<OrderDTO>>> GetAllOrders()
    {
        var OrderList = await _Order.GetList();

        var dtoList = OrderList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{order_id}")]

    public async Task<ActionResult<OrderDTO>> GetById([FromRoute] long order_id)
    {
        var Order = await _Order.GetById(order_id);
        if (Order is null)
            return NotFound("No Order found with given employee number");
            var dto =Order.asDto;
            dto.Product = await _product.GetOrderById(order_id);
          
            
        return Ok(dto);
    } 
               
                         
    [HttpPost]

    public async Task<ActionResult<OrderDTO>> CreateOrder([FromBody] CreateOrderDTO Data)
    {
        // if (!(new string[] { "male", "female" }.Contains(Data.Gender.Trim().ToLower())))
            // return BadRequest("Gender value is not recognized");

        // var subtractDate = DateTimeOffset.Now - Data.DateOfBirth;
        // if (subtractDate.TotalDays / 365 < 18.0)
        // return BadRequest("Employee must be at least 18 years old");/

        var toCreateOrder = new Order
        {
            
           OrderId = Data.OrderId,
            OrderNo = Data.OrderNo,
            // DateOfBirth = Data.DateOfBirth.UtcDateTime,
            Quantity = Data.Quantity,
            // OrderBrand=Data.OrderBrand.Trim().ToLower(),
            CustomerId=Data.CustomerId,
              
        };
        var createdOrder = await _Order.Create(toCreateOrder);

        return StatusCode(StatusCodes.Status201Created, createdOrder.asDto);


    }

    [HttpPut("{order_id}")]


    public async Task<ActionResult> UpdateOrder([FromRoute] long OrderId,
    [FromBody] OrderUpdateDTO Data)
    {
        var existing = await _Order.GetById(OrderId);
        if (existing is null)
            return NotFound("No Order found with given customer number");

        var toUpdateOrder = existing with
        {
            // Email = Data.Email?.Trim()?.ToLower() ?? existing.Email,
            // LastName = Data.LastName?.Trim() ?? existing.LastName,
            // Mobile = Data.Mobile ?? existing.Mobile,
            // DateOfBirth = existing.DateOfBirth.UtcDateTime,
        };

        var didUpdate = await _Order.Update(toUpdateOrder);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update");
        return NoContent();
    }

    [HttpDelete("{order_id}")]
    public async Task<ActionResult> DeleteOrder([FromRoute] long OrderId)
    {
        var existing = await _Order.GetById(OrderId);
        if (existing is null)
            return NotFound("No Order found with given employee number");
        await _Order.Delete(OrderId);
        return NoContent();
    }



}