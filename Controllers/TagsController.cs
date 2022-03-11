using Microsoft.AspNetCore.Mvc;
using Postdb.DTOs;
using Postdb.Models;
using Postdb.Repositories;

namespace Postdb.Controllers;

[ApiController]
[Route("api/tags")]
public class TagsController : ControllerBase
{
    private readonly ILogger<TagsController> _logger;
    private readonly ITagsRepository _Tags;

    public TagsController(ILogger<TagsController> logger, ITagsRepository Tags)
    {
        _logger = logger;
        _Tags = Tags;
    }
    [HttpGet]
    public async Task<ActionResult<List<ProductDTO>>> GetAllTags()
    {
        var TagsList = await _Tags.GetList();

        var dtoList =  TagsList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{tags_id}")]

    public async Task<ActionResult<TagsDTO>> GetById([FromRoute] long tags_id)
    {
        var Tags = await _Tags.GetById(tags_id);
        if (Tags is null)
            return NotFound("No Product found with given employee number");
        return Ok(Tags.asDto);
    }

    [HttpPost]

    public async Task<ActionResult<TagsDTO>> CreateTags([FromBody] CreateTagsDTO Data)
    {
        // if (!(new string[] { "male", "female" }.Contains(Data.Gender.Trim().ToLower())))
        // return BadRequest("Gender value is not recognized");

        // var subtractDate = DateTimeOffset.Now - Data.DateOfBirth;
        // if (subtractDate.TotalDays / 365 < 18.0)
        // return BadRequest("Employee must be at least 18 years old");/

        var toCreateTags= new Tags
        {

            TagId = Data.TagId,
            ProductId = Data.ProductId,
            // DateOfBirth = Data.DateOfBirth.UtcDateTime,
            // ProductSize = Data.ProductSize,
            Title = Data.Title.Trim().ToLower(),
            Colour=Data.Colour,
            TagSize=Data.TagSize,

        };
        var createdTags = await _Tags.Create(toCreateTags);

        return StatusCode(StatusCodes.Status201Created, createdTags.asDto);


    }

    [HttpPut("{tags_id}")]
    public async Task<ActionResult> UpdateTags([FromRoute] long TagsId,
    [FromBody] TagsUpdateDTO Data)
    {
        var existing = await _Tags.GetById(TagsId);
        if (existing is null)
            return NotFound("No Product found with given customer number");

        var toUpdateTags = existing with
        {
            // Email = Data.Email?.Trim()?.ToLower() ?? existing.Email,
            // LastName = Data.LastName?.Trim() ?? existing.LastName,
            // Mobile = Data.Mobile ?? existing.Mobile,
            // DateOfBirth = existing.DateOfBirth.UtcDateTime,
        };

        var didUpdate = await _Tags.Update(toUpdateTags);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update");
        return NoContent();
    }

    [HttpDelete("{tags_id}")]
    public async Task<ActionResult> DeleteTags([FromRoute] long TagsId)
    {
        var existing = await _Tags.GetById(TagsId);
        if (existing is null)
            return NotFound("No Product found with given employee number");
        await _Tags.Delete(TagsId);
        return NoContent();
    }



}

