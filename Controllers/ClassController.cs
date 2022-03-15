using Microsoft.AspNetCore.Mvc;
// using Postdb.DTOs;
using School.Models;
using School.Repositories;
using School.DTOs;

namespace Postdb.Controllers;

[ApiController]
[Route("api/Class")]
public class ClassController : ControllerBase
{
    private readonly ILogger<ClassController> _logger;
    private readonly IClassRepository _class;

    public ClassController(ILogger<ClassController> logger, IClassRepository Class)
    {
        _logger = logger;
        _class = Class;
    }
    [HttpGet]
    public async Task<ActionResult<List<ClassDTO>>> GetAllClass()
    {
        var ClassList = await _class.GetList();

        var dtoList =  ClassList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{Class_id}")]

    public async Task<ActionResult<ClassDTO>> GetById([FromRoute] long Class_id)
    {
        var Class = await _class.GetById(Class_id);
        if (Class is null)
            return NotFound("No Product found with given employee number");
        return Ok(Class.asDto);
    }

    [HttpPost]

    public async Task<ActionResult<ClassDTO>> CreateClass([FromBody] CreateClassDTO Data)
    {
        // if (!(new string[] { "male", "female" }.Contains(Data.Gender.Trim().ToLower())))
        // return BadRequest("Gender value is not recognized");

        // var subtractDate = DateTimeOffset.Now - Data.DateOfBirth;
        // if (subtractDate.TotalDays / 365 < 18.0)
        // return BadRequest("Employee must be at least 18 years old");/

        var toCreateClass= new Class
        {

        //    ClassId=Data.ClassId,
        //    FirstName=Data.FirstName,
        //    LastName=Data.LastName,
        //    DateOfBirth=Data.DateOfBirth,
        //    Gender=Data.Gender,
        //    Mobile=Data.Mobile,

        };
        var createdClass = await _class.Create(toCreateClass);

        return StatusCode(StatusCodes.Status201Created, createdClass.asDto);


    }

    [HttpPut("{Class_id}")]
    public async Task<ActionResult> UpdateClass([FromRoute] long Class_id,
    [FromBody] ClassUpdateDTO Data)
    {
        var existing = await _class.GetById(Class_id);
        if (existing is null)
            return NotFound("No Product found with given customer number");

        var toUpdateClass = existing with
        {
            // Email = Data.Email?.Trim()?.ToLower() ?? existing.Email,
            // LastName = Data.LastName?.Trim() ?? existing.LastName,
            // Mobile = Data.Mobile ?? existing.Mobile,
            // DateOfBirth = existing.DateOfBirth.UtcDateTime,
        };

        var didUpdate = await _class.Update(toUpdateClass);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update");
        return NoContent();
    }

    [HttpDelete("{Class_id}")]
    public async Task<ActionResult> DeleteClass([FromRoute] long ClassId)
    {
        var existing = await _class.GetById(ClassId);
        if (existing is null)
            return NotFound("No Product found with given employee number");
        await _class.Delete(ClassId);
        return NoContent();
    }



}

