using Postdb.Models;
using Dapper;
using Postdb.Utilities;

namespace Postdb.Repositories;


public interface ICustomerRepository
{
    Task<Customer> Create(Customer Item);
    Task<bool> Update(Customer item);
    Task<bool> Delete(long CustomerId);
    Task<Customer> GetById(long CustomerId);
    Task<List<Customer>> GetList();
    Task<List<Order>> GetOrders();
}
public class CustomerRepository : BaseRepository, ICustomerRepository
{
    public CustomerRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Customer> Create(Customer item)
    {


        var query = $@"INSERT INTO ""{TableNames.customer}""
        (first_name, last_name, mobile, email, address, gender)
        VALUES (@FirstName, @LastName,  @Mobile, @Email, @Address, @Gender) RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Customer>(query, item);

            return res;
        }

    }

    public async Task<bool> Delete(long CustomerId)
    {
        var query = $@"DELETE FROM ""{TableNames.customer}""
        WHERE CustomerId = @CustomerId";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { CustomerId });
            return res > 0;
        }
    }

    public async Task<Customer> GetById(long customerId)
    {
        var query = $@"SELECT * FROM ""{TableNames.customer}""
        WHERE customer_id = @customerId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Customer>(query, new
            {
                customerid = customerId
            });

    }

    public async Task<List<Customer>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.customer}""";
        List<Customer> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Customer>(query)).AsList();
        return res;
    }

    public Task<List<Order>> GetOrders()
    {
        return null;
    }

    public async Task<bool> Update(Customer item)
    {
        var query = $@"UPDATE ""{TableNames.customer}"" SET 
         mobile = @Mobile, email = @Email,  WHERE CustomerId = @CustomerId";


        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, item);
            return rowCount == 1;

        }
    }
}
