using Postdb.Models;
using Dapper;
using Postdb.Utilities;
using Postdb.DTOs;

namespace Postdb.Repositories;


public interface IProductRepository
{
    Task<Product> Create(Product Item);
    Task<bool> Update(Product item);
    Task<bool> Delete(long ProductId);
    Task<Product> GetById(long ProductId);
    Task<List<Product>> GetList();   
    Task<List<ProductDTO>> GetOrderById( long OrderId);   
}
public class ProductRepository : BaseRepository, IProductRepository
{
    public ProductRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Product> Create(Product item)
    {


        var query = $@"INSERT INTO ""{TableNames.product}""
        (product_id,product_name,product_brand,order_id,customer_id)
        VALUES (@ProductId,  @ProductName, @ProductBrand, @OrderId, @CustomerId) RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Product>(query, item);

            return res;
        }

    }   

    public async Task<bool> Delete(long ProductId)
    {
        var query = $@"DELETE FROM ""{TableNames.product}""   

        WHERE ProductId = @ProductId";
 
        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { ProductId });
            return res > 0;
        }
    }

    public async Task<Product> GetById(long ProductId)
    {
        var query = $@"SELECT * FROM ""{TableNames.product}""
        WHERE Product_id = @ProductId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Product>(query, new
            {
                Productid = ProductId
            });

    }

    public async Task<List<Product>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.product}""";
        List<Product> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Product>(query)).AsList();
        return res;
    }

    public  async Task<List<ProductDTO>> GetOrderById(long OrderId)
    {
        var query = $@"SELECT p. * FROM {TableNames.order_product} op
        LEFT JOIN {TableNames.product} p ON p.product_id = op.product_id
        WHERE op.order_id = @OrderId ";

        using(var con = NewConnection)
        {
            return (await con.QueryAsync<ProductDTO>(query, new{OrderId})).AsList();
        }
    }

    public async Task<bool> Update(Product item)
    {
        var query = $@"UPDATE ""{TableNames.product}"" SET 
         mobile = @Mobile, email = @Email,  WHERE ProductId = @ProductId";


        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, item);
            return rowCount == 1;

        }
    }
}