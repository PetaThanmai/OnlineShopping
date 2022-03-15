using Microsoft.AspNetCore.Mvc;
// using Postdb.DTOs;
// using Postdb.Models;
// using Postdb.Repositories;
using School.DTOs;
using School.Models;
using School.Repositories;

namespace Postdb.Controllers;

[ApiController]
[Route("api/teacher")]
public class teacherController : ControllerBase
{
    private readonly ILogger<teacherController> _logger;
    private readonly ITeacherRepository _Teacher;
    private readonly IStudentRepository _Student;
    private readonly ISubjectRepository _Subject;
    // private readonly ITeacherRepository _order;

    public teacherController(ILogger<teacherController> logger, ITeacherRepository teacher,IStudentRepository Student,ISubjectRepository Subject)
    {
        _logger = logger;
        _Teacher = teacher;
        _Student = Student;
        _Subject = Subject;
    
    }
    // [HttpGet]
    // public async Task<ActionResult<List<TeacherDTO>>> GetAllteachers()
    // {
    //     var teacherList = await _Teacher.GetList();

    //     var dtoList = TeacherList.Select(x => x.asDto);

    //     return Ok(dtoList);
    // }

    [HttpGet("{teacher_id}")]

    public async Task<ActionResult<TeacherDTO>> GetById([FromRoute] long teacher_id)
    {
        //Request validation
        //Database connection 
        //sql query
        //query execute
        //Response handle
        //Dto binding
        var teacher = await _Teacher.GetById(teacher_id);
        if (teacher is null)
            return NotFound("No teacher found with given employee number");
        var dto = teacher.asDto;
        dto.Student = (await _Student.GetListOfTeacher(teacher_id)).Select(x => x.asDto).ToList();
        dto.Subject = (await _Subject.GetListOfSubjects(teacher_id)).Select(x => x.asDto).ToList();

        // asDto.MyOrders = await _order.GetList(teacher_id);
        // OrderDTO.Orders =await _order.GetList(order_id);
        return Ok(dto);
    }

    [HttpPost]

    public async Task<ActionResult<TeacherDTO>> Createteacher([FromBody] CreateTeacherDTO Data)
    {
        if (!(new string[] { "male", "female" }.Contains(Data.Gender.ToString())))
            return BadRequest("Gender value is not recognized");

        // var subtractDate = DateTimeOffset.Now - Data.DateOfBirth;
        // if (subtractDate.TotalDays / 365 < 18.0)
        // return BadRequest("Employee must be at least 18 years old");/

        var toCreateteacher = new Teacher
        {
            TeacherId = Data.TeacherId,
            TeacherName = Data.TeacherName,
            // DateOfBirth = Data.DateOfBirth.UtcDateTime,
            TeacherSub= Data.TeacherSub,
            // subjectBrand=Data.subjectBrand.Trim().ToLower(),
            Mobile=Data.Mobile,
            Gender=Data.Gender,
              

        };
        var createdteacher = await _Teacher.Create(toCreateteacher);

        return StatusCode(StatusCodes.Status201Created, createdteacher.asDto);


    }

    [HttpPut("{teacher_id}")]
    public async Task<ActionResult> Updateteacher([FromRoute] long teacher_id,
    [FromBody] TeacherUpdateDTO Data)
    {
        var existing = await _Teacher.GetById(teacher_id);
        if (existing is null)
            return NotFound("No teacher found with given employee number");

        var toUpdateteacher = existing with
        {
            // Email = Data.Email?.Trim()?.ToLower() ?? existing.Email,
            // LastName = Data.LastName?.Trim() ?? existing.LastName,
            Mobile = Data.Mobile ?? existing.Mobile,
            TeacherSub = Data.TeacherSub,
            // DateOfBirth = existing.DateOfBirth.UtcDateTime,
        };

        var didUpdate = await _Teacher.Update(toUpdateteacher);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update");
        return NoContent();
    }

    [HttpDelete("{teacher_id}")]
    public async Task<ActionResult> Deleteteacher([FromRoute] long teacher_id)
    {
        var existing = await _Teacher.GetById(teacher_id);
        if (existing is null)
            return NotFound("No teacher found with given employee number");
        await _Teacher.Delete(teacher_id);
        return NoContent();
    }



}
