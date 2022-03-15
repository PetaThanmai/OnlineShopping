using School.Models;
using Dapper;
// using School.Utilities;
using School.DTOs;
using School.Utilites;
// using School.DTOs;

namespace School.Repositories;


public interface IStudentRepository
{
    Task<Student> Create(Student Item);
    Task<bool> Update(Student item);
    Task<bool> Delete(long StudentId);
    Task<Student> GetById(long StudentId);
    Task<List<Student>> GetList();   
    Task<List<Student>> GetListOfTeacher(long TeacherId);   
    // Task<List<StudentDTO>> GetOrderById( long OrderId);   
}
public class StudentRepository : BaseRepository, IStudentRepository
{
    

    public StudentRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Student> Create(Student item)
    {


        var query = $@"INSERT INTO ""{TableNames.student}""
        (student_id,first_name,last_name,date_of_birth,gender,mobile,class_id,teacher_id)
        VALUES (@StudentId,  @FirstName, @LastName, @DateOfBirth,@Gender, @Mobile, @ClassId, @TeacherId) RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Student>(query, item);

            return res;
        }

    }   

    public async Task<bool> Delete(long StudentId)
    {
        var query = $@"DELETE FROM ""{TableNames.student}""   

        WHERE Student_id = @StudentId";
 
        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { StudentId });
            return res > 0;
        }
    }

    public async Task<Student> GetById(long StudentId)
    {
        var query = $@"SELECT * FROM ""{TableNames.student}""
        WHERE Student_id = @StudentId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Student>(query, new
            {
                Studentid = StudentId
            });

    }

    public async Task<List<Student>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.student}""";
        List<Student> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Student>(query)).AsList();
        return res;
    }

    

    public async Task<bool> Update(Student item)
    {
        var query = $@"UPDATE ""{TableNames.student}"" SET 
        mobile=@Mobile,date_of_birth = @DateOfBirth  WHERE Student_id = @StudentId";


        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, item);
            return rowCount == 1;

        }
    }

    public async Task<List<Student>>GetListOfTeacher(long TeacherId)
    {
        var query = $@"SELECT s.* FROM {TableNames.stu_tea} st
        LEFT JOIN {TableNames.student} s ON s.student_id = st.student_id
        WHERE st.teacher_id = @TeacherId ";

        using(var con = NewConnection)
        {
            return (await con.QueryAsync<Student>(query, new{TeacherId})).AsList();
        }
    }
}