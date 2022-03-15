using Microsoft.AspNetCore.Mvc;
// using Postdb.DTOs;
// using Postdb.Models;
// using Postdb.Repositories;
using School.DTOs;
using School.Models;
using School.Repositories;

namespace Postdb.Controllers;

[ApiController]
[Route("api/subject")]
public class subjectController : ControllerBase
{
    private readonly ILogger<subjectController> _logger;
    private readonly ISubjectRepository _subject;
    // private readonly IProductRepository _product;

// private readonly ICustomerRepository _customer;

    public subjectController(ILogger<subjectController> logger, ISubjectRepository subject)
    {
        _logger = logger;
        _subject = subject;
    //     _product=product;
    //     _customer=customer;
    }
    [HttpGet]
    public async Task<ActionResult<List<SubjectDTO>>> GetAllsubjects()
    {
        var subjectList = await _subject.GetList();

        var dtoList = subjectList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{subject_id}")]

    public async Task<ActionResult<SubjectDTO>> GetById([FromRoute] long subject_id)
    {
        var subject = await _subject.GetById(subject_id);
        if (subject is null)
            return NotFound("No subject found with given employee number");
            var dto =subject.asDto;
            // dto.Product = await _product.GetsubjectById(subject_id);
          
            
        return Ok(dto);
    } 
               
                         
    [HttpPost]

    public async Task<ActionResult<SubjectDTO>> Createsubject([FromBody] CreateSubjectDTO Data)
    {
        // if (!(new string[] { "male", "female" }.Contains(Data.Gender.Trim().ToLower())))
            // return BadRequest("Gender value is not recognized");

        // var subtractDate = DateTimeOffset.Now - Data.DateOfBirth;
        // if (subtractDate.TotalDays / 365 < 18.0)
        // return BadRequest("Employee must be at least 18 years old");/

        var toCreatesubject = new Subject
        {
            
           SubjectId = Data.SubjectId,
            SubName = Data.SubName,
            // DateOfBirth = Data.DateOfBirth.UtcDateTime,
        // SubTeacher= Data.SubTeacher,
            // subjectBrand=Data.subjectBrand.Trim().ToLower(),
            // Mobile=Data.Mobile,
            // Gender=Data.Gender,
              
        };
        var createdsubject = await _subject.Create(toCreatesubject);

        return StatusCode(StatusCodes.Status201Created, createdsubject.asDto);


    }

    [HttpPut("{subject_id}")]


    public async Task<ActionResult> Updatesubject([FromRoute] long subject_id,
    [FromBody] SubjectUpdateDTO Data)
    {
        var existing = await _subject.GetById(subject_id);
        if (existing is null)
            return NotFound("No subject found with given customer number");

        var toUpdatesubject = existing with
        
        {
            // SubjectId = Data.SubjectId,
            SubName = Data.SubName,
            // Email = Data.Email?.Trim()?.ToLower() ?? existing.Email,
            // LastName = Data.LastName?.Trim() ?? existing.LastName,
            // Mobile = Data.Mobile ?? existing.Mobile,
            // DateOfBirth = existing.DateOfBirth.UtcDateTime,
        };

        var didUpdate = await _subject.Update(toUpdatesubject);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update");
        return NoContent();
    }

    [HttpDelete("{subject_id}")]
    public async Task<ActionResult> Deletesubject([FromRoute] long subject_id)
    {
        var existing = await _subject.GetById(subject_id);
        if (existing is null)
            return NotFound("No subject found with given employee number");
        await _subject.Delete(subject_id);
        return NoContent();
    }



}