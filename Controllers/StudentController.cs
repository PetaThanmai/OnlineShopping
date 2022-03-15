using Microsoft.AspNetCore.Mvc;
// using Postdb.DTOs;
using School.Models;
using School.Repositories;
using School.DTOs;

namespace School.Controllers;

[ApiController]
[Route("api/student")]
public class StudentController : ControllerBase
{
    private readonly ILogger<StudentController> _logger;
    private readonly IStudentRepository _student;
    private readonly ITeacherRepository _teacher;
    private readonly ISubjectRepository _subject;


    public StudentController(ILogger<StudentController> logger, IStudentRepository student, ITeacherRepository teacher , ISubjectRepository subject)

    {
        _logger = logger;
        _student = student;
        _teacher = teacher;
        _subject = subject;
    }
    [HttpGet]
    public async Task<ActionResult<List<StudentDTO>>> GetAllstudent()
    {
        var studentList = await _student.GetList();

        var dtoList = studentList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{student_id}")]

    public async Task<ActionResult<StudentDTO>> GetById([FromRoute] long student_id)
    {
        var student = await _student.GetById(student_id);
        if (student is null)
            return NotFound("No Product found with given employee number");
        var dto = student.asDto;

        dto.Teacher = (await _teacher.GetListOfTeacher(student_id)).Select(x => x.asDto).ToList();
        dto.Subject = (await _subject.SubjectsByStudentId(student_id)).Select(x => x.asDto).ToList();

        return Ok(dto);
    }

    [HttpPost]

    public async Task<ActionResult<StudentDTO>> Createstudent([FromBody] CreateStudentDTO Data)
    {
        // if (!(new string[] { "male", "female" }.Contains(Data.Gender.Trim().ToLower())))
        // return BadRequest("Gender value is not recognized");

        // var subtractDate = DateTimeOffset.Now - Data.DateOfBirth;
        // if (subtractDate.TotalDays / 365 < 18.0)
        // return BadRequest("Employee must be at least 18 years old");/

        var toCreatestudent = new Student
        {

            StudentId = Data.StudentId,
            FirstName = Data.FirstName,
            LastName = Data.LastName,
            DateOfBirth = Data.DateOfBirth,
            Gender = Data.Gender,
            Mobile = Data.Mobile,
            ClassId = Data.ClassId,
            TeacherId = Data.TeacherId,

        };
        var createdstudent = await _student.Create(toCreatestudent);

        return StatusCode(StatusCodes.Status201Created, createdstudent.asDto);


    }

    [HttpPut("{student_id}")]
    public async Task<ActionResult> Updatestudent([FromRoute] long student_id,
    [FromBody] StudentUpdateDTO Data)
    {
        var existing = await _student.GetById(student_id);
        if (existing is null)
            return NotFound("No Product found with given customer number");


        var toUpdatestudent = existing with
        {
            // Email = Data.Email?.Trim()?.ToLower() ?? existing.Email,
            // LastName = Data.LastName?.Trim() ?? existing.LastName,
            Mobile = Data.Mobile,
            DateOfBirth = existing.DateOfBirth.UtcDateTime,
        };

        var didUpdate = await _student.Update(toUpdatestudent);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update");
        return NoContent();
    }

    [HttpDelete("{student_id}")]
    public async Task<ActionResult> Deletestudent([FromRoute] long studentId)
    {
        var existing = await _student.GetById(studentId);
        if (existing is null)
            return NotFound("No Product found with given employee number");
        await _student.Delete(studentId);
        return NoContent();
    }



}

