using School.Models;
using Dapper;
// using School.Utilities;
using School.Utilites;

namespace School.Repositories;


public interface ITeacherRepository
{
    Task<Teacher> Create(Teacher Item);
    Task<bool> Update(Teacher item);
    Task<bool> Delete(long TeacherId);
    Task<Teacher> GetById(long TeacherId);
    // Task GetList();
    Task<List<Teacher>> GetListOfTeacher(long StudentId);
    // Task<List<Order>> GetOrders();
}
public class TeacherRepository : BaseRepository, ITeacherRepository
{
    public TeacherRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Teacher> Create(Teacher item)
    {


        var query = $@"INSERT INTO ""{TableNames.teacher}""
        (teacher_id,teacher_name,teacher_sub,mobile,gender)
        VALUES (@TeacherId,@TeacherName, @TeacherSub,  @Mobile, @Gender) RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Teacher>(query, item);

            return res;
        }

    }

    public async Task<bool> Delete(long TeacherId)
    {
        var query = $@"DELETE FROM ""{TableNames.teacher}""
        WHERE teacher_id = @TeacherId";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { TeacherId });
            return res > 0;
        }
    }

    public async Task<Teacher> GetById(long TeacherId)
    {
        var query = $@"SELECT * FROM ""{TableNames.teacher}""
        WHERE teacher_id = @TeacherId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Teacher>(query, new
            {
                Teacherid = TeacherId
            });

    }

    // public async Task<List<Teacher>> GetList()
    // {
    //     var query = $@"SELECT * FROM ""{TableNames.teacher}""";
    //     List<Teacher> res;
    //     using (var con = NewConnection)
    //         res = (await con.QueryAsync<Teacher>(query)).AsList();
    //     return res;
    // }

    // public Task<List<Order>> GetOrders()
    // {
    //     return null;
    // }

    public async Task<bool> Update(Teacher item)
    {
        var query = $@"UPDATE ""{TableNames.teacher}"" SET teacher_sub = @TeacherSub,
         mobile = @Mobile WHERE teacher_id = @TeacherId";


        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, item);
            return rowCount == 1;

        }
    }

     public async Task<List<Teacher>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.teacher}""";
        List<Teacher> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Teacher>(query)).AsList();
        return res;
    }

    

    public async Task<List<Teacher>> GetListOfTeacher(long StudentId)
    {
        var query = $@"SELECT t.* FROM {TableNames.stu_tea} st
        LEFT JOIN {TableNames.teacher} t ON t.teacher_id = st.teacher_id
        WHERE st.student_id = @StudentId";

        using(var con = NewConnection)
        {
            return (await con.QueryAsync<Teacher>(query,new{StudentId})).AsList();
        }


    }
}
