// using School.Models;
using Dapper;
using School.Models;
// using School.Utilities;
// using School.Utilities;
using School.DTOs;
using School.Utilites;

namespace School.Repositories;


public interface IClassRepository
{
    Task<Class> Create(Class Item);
    Task<bool> Update(Class item);
    Task<bool> Delete(long ClassId);
    Task<Class> GetById(long ClassId);
    Task<List<Class>> GetList();
    // Task<List<ClassDTO>> GetList(object ClassId);
}
public class ClassRepository : BaseRepository, IClassRepository
{
    public ClassRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Class> Create(Class item)
    {


        var query = $@"INSERT INTO ""{TableNames.@class}""
        (Class_id,capacity,room_type)
        VALUES (@ClassId,  @Capacity, @RoomType) RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Class>(query, item);

            return res;
        }

    }

    public async Task<bool> Delete(long ClassId)
    {
        var query = $@"DELETE FROM ""{TableNames.@class}""
        WHERE Class_id = @ClassId";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { ClassId });
            return res > 0;
        }
    }

    public async Task<Class> GetById(long ClassId)
    {
        var query = $@"SELECT * FROM ""{TableNames.@class}""
        WHERE class_id = @ClassId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Class>(query, new
            {
                Classid = ClassId
            });

    }

    public async Task<List<Class>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.@class}""";
        List<Class> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Class>(query)).AsList();
        return res;
    }

    // public Task<List<ClassDTO>> GetList(object ClassId)
    // {

    //     return null;
    // }

    public async Task<bool> Update(Class item)
    {
        var query = $@"UPDATE ""{TableNames.@class}"" SET 
         capacity = @Capacity,room_type = @RoomType WHERE Class_id = @ClassId";

        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, item);
            return rowCount == 1;
        }
    }
}