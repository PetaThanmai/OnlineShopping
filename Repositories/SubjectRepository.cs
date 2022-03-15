using School.Models;
using Dapper;
// using School.Utilities;
// using School.Models;
using School.DTOs;
using School.Utilites;
// using School.DTOs;

namespace School.Repositories;


public interface ISubjectRepository
{
    Task<Subject> Create(Subject Item);
    Task<bool> Update(Subject item);
    Task<bool> Delete(long SubjectId);
    Task<Subject> GetById(long SubjectId);
    Task<List<Subject>> GetList();
    Task<List<Subject>> GetListOfSubjects(long TeacherId);
    Task<List<Subject>> SubjectsByStudentId(long StudentId);
    // Task<List<Teacher>> GetListOfTeacher(long StudentId);
    // Task<List<SubjectDTO>> GetList(long CustomerId);

}
public class SubjectRepository : BaseRepository, ISubjectRepository
{
    public SubjectRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Subject> Create(Subject item)
    {


        var query = $@"INSERT INTO ""{TableNames.subject}""
         (subject_id,sub_name)
         VALUES (@SubjectId, @SubName) RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Subject>(query, item);

            return res;
        }

    }

    public async Task<bool> Delete(long SubjectId)
    {
        var query = $@"DELETE FROM ""{TableNames.subject}""
        WHERE Subject_id = @SubjectId";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { SubjectId });
            return res > 0;
        }
    }

    public async Task<Subject> GetById(long SubjectId)
    {
        var query = $@"SELECT * FROM ""{TableNames.subject}""
        WHERE Subject_id = @SubjectId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Subject>(query, new
            {
                SubjectId = SubjectId
            });

    }

    public async Task<List<Subject>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.subject}""";
        List<Subject> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Subject>(query)).AsList();
        return res;
    }

    public async Task<List<Subject>> GetListOfSubjects(long TeacherId)
    {
        var query = $@"SELECT * FROM ""{TableNames.subject}"" s
        LEFT JOIN {TableNames.teacher} t ON s.subject_id = s.subject_id
        WHERE t.teacher_id = @TeacherId";
        using (var con = NewConnection)
        {
            return(await con.QueryAsync<Subject>(query, new{TeacherId})).AsList();
        
        }
        
    }

    // public async Task<List<Teacher>> GetListOfTeacher(long StudentId)
    // {
    //     var query = $@"SELECT t.* FROM {TableNames.stu_tea} st
    //     LEFT JOIN {TableNames.teacher} t ON t.teacher_id = st.teacher_id
    //     WHERE st.student_id = @StudentId";

    //     using(var con = NewConnection)
    //     {
    //         return (await con.QueryAsync<Teacher>(query,new{StudentId})).AsList();
    //     }

    // }

    public async Task<List<Subject>>SubjectsByStudentId(long StudentId)
    {
    var query = $@"SELECT * FROM ""{TableNames.stu_sub}"" ss
       LEFT JOIN {TableNames.subject} s ON s.subject_id = ss.sub_id
        WHERE ss.student_id = @StudentId";

        using(var con = NewConnection)
        {
            return (await con.QueryAsync<Subject>(query,new{StudentId})).AsList();
         }
    }

    // public async Task<List<SubjectDTO>> GetList(long CustomerId)
    // {
    //     var query = $@"SELECT * FROM ""{TableNames.subject}""
    //      WHERE customer_id= @CustomerId";

    //     using (var con = NewConnection)
    //     {
    //         return (await con.QueryAsync<SubjectDTO>(query, new { CustomerId })).AsList();
    //     }
    // }

    // var query = $@"SELECT product.* FROM {TableNames.Subject_product}  Subjectprod INNER JOIN {TableNames.product} product 
    //   on Subjectprod.product_id = product.product_id where Subjectprod.Subject_id =@SubjectId Subject by product.product_id asc;
    //   ";

    public async Task<bool> Update(Subject item)
    {
        var query = $@"UPDATE ""{TableNames.subject}"" SET sub_name = @SubName
           WHERE subject_id = @SubjectId";


        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, item);
            return rowCount == 1;

        }
    }
}