using Microsoft.AspNetCore.Mvc;
using Postdb.DTOs;
using Postdb.Models;
using Postdb.Repositories;

namespace Postdb.Controllers;

[ApiController]
[Route("api/product")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductRepository _Product;
    private readonly IOrderRepository _Order;
    private readonly ITagsRepository _tags;


    public ProductController(ILogger<ProductController> logger, IProductRepository Product ,IOrderRepository Order,ITagsRepository tags)
    {
        _logger = logger;
        _Product = Product;
        _Order=Order;
        _tags=tags;
    }
    [HttpGet]
    public async Task<ActionResult<List<ProductDTO>>> GetAllProducts()
    {
        var ProductList = await _Product.GetList();

        var dtoList = ProductList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{product_id}")]

    public async Task<ActionResult<ProductDTO>> GetById([FromRoute] long product_id)
    {
        var Product = await _Product.GetById(product_id);
        if (Product is null)
            return NotFound("No Product found with given employee number");
            var dto =Product.asDto;
           // var dto=Tags.asDto;
            dto.Order = await _Order.GetList(Product.ProductId);
          dto.Tags = await _tags.GetList();

        return Ok(dto);
                                     
        // return Ok(Product.asDto);  
    }

    [HttpPost]

    public async Task<ActionResult<ProductDTO>> CreateProduct([FromBody] CreateProductDTO Data)
    {
        // if (!(new string[] { "male", "female" }.Contains(Data.Gender.Trim().ToLower())))
        // return BadRequest("Gender value is not recognized");

        // var subtractDate = DateTimeOffset.Now - Data.DateOfBirth;
        // if (subtractDate.TotalDays / 365 < 18.0)
        // return BadRequest("Employee must be at least 18 years old");/

        var toCreateProduct = new Product
        {

            ProductName = Data.ProductName.Trim(),
            // ProductType = Data.ProductType.ToString(),
            // DateOfBirth = Data.DateOfBirth.UtcDateTime,
            // ProductSize = Data.ProductSize,
            ProductBrand = Data.ProductBrand.Trim().ToLower(),
            OrderId=Data.OrderId,
            CustomerId=Data.CustomerId,

        };
        var createdProduct = await _Product.Create(toCreateProduct);

        return StatusCode(StatusCodes.Status201Created, createdProduct.asDto);


    }

    [HttpPut("{product_id}")]
    public async Task<ActionResult> UpdateProduct([FromRoute] long ProductId,
    [FromBody] ProductUpdateDTO Data)
    {
        var existing = await _Product.GetById(ProductId);
        if (existing is null)
            return NotFound("No Product found with given customer number");

        var toUpdateProduct = existing with
        {
            // Email = Data.Email?.Trim()?.ToLower() ?? existing.Email,
            // LastName = Data.LastName?.Trim() ?? existing.LastName,
            // Mobile = Data.Mobile ?? existing.Mobile,
            // DateOfBirth = existing.DateOfBirth.UtcDateTime,
        };

        var didUpdate = await _Product.Update(toUpdateProduct);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update");
        return NoContent();
    }

    [HttpDelete("{product_id}")]
    public async Task<ActionResult> DeleteProduct([FromRoute] long ProductId)
    {
        var existing = await _Product.GetById(ProductId);
        if (existing is null)
            return NotFound("No Product found with given employee number");
        await _Product.Delete(ProductId);
        return NoContent();
    }



}