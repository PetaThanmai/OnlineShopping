using Postdb.Models;
using Dapper;
using Postdb.Utilities;
using Postdb.DTOs;

namespace Postdb.Repositories;


public interface IOrderRepository
{
    Task<Order> Create(Order Item);
    Task<bool> Update(Order item);
    Task<bool> Delete(long OrderId);
    Task<Order> GetById(long OrderId);
    Task<List<Order>> GetList();
    Task<List<OrderDTO>> GetList(long CustomerId);

}
public class OrderRepository : BaseRepository, IOrderRepository
{
    public OrderRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Order> Create(Order item)
    {


        var query = $@"INSERT INTO ""{TableNames.order}""
         (order_id,order_no,quantity,customer_id)
         VALUES (@OrderId, @OrderNo, @Quantity, @CustomerId) RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Order>(query, item);

            return res;
        }

    }
              
    public async Task<bool> Delete(long OrderId)
    {
        var query = $@"DELETE FROM ""{TableNames.order}""
        WHERE OrderId = @OrderId";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { OrderId });
            return res > 0;
        }
    }

    public async Task<Order> GetById(long OrderId)
    {
        var query = $@"SELECT * FROM ""{TableNames.order}""
        WHERE Order_id = @OrderId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Order>(query, new
            {
                OrderId = OrderId
            });

    }

    public async Task<List<Order>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.order}""";
        List<Order> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Order>(query)).AsList();
        return res;
    }

    public async Task<List<OrderDTO>> GetList(long CustomerId) 
    {
        var query = $@"SELECT * FROM ""{TableNames.order}""
         WHERE customer_id= @CustomerId";
;

        using (var con = NewConnection)
        {
            return(await con.QueryAsync<OrderDTO>(query,new{CustomerId})).AsList();
        }
    }

            // var query = $@"SELECT product.* FROM {TableNames.order_product}  orderprod INNER JOIN {TableNames.product} product 
			//   on orderprod.product_id = product.product_id where orderprod.order_id =@OrderId order by product.product_id asc;
			//   ";

    public async Task<bool> Update(Order item)
    {
        var query = $@"UPDATE ""{TableNames.order}"" SET 
         mobile = @Mobile, email = @Email,  WHERE OrderId = @OrderId";


        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, item);
            return rowCount == 1;

        }
    }
}