using dotnet_oev_backend_rest.Dtos.Request;
using dotnet_oev_backend_rest.Dtos.Response;
using dotnet_oev_backend_rest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_oev_backend_rest.Controllers;

[ApiController]
[Route("api/lesson")]
public class LessonsController : ControllerBase
{
    private readonly ILessonService _lessonService;

    public LessonsController(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    [HttpGet("findLessonById/{lessonId:long}", Name = nameof(FindLessonByIdAsync))]
    public async Task<ActionResult<LessonResponseDTO>> FindLessonByIdAsync(long lessonId)
    {
        var lesson = await _lessonService.FindLessonByIdAsync(lessonId);
        return Ok(lesson);
    }


    [HttpGet("findLessonsByCourseId/{courseId:long}")]
    public async Task<IReadOnlyList<LessonResponseDTO>> FindLessonsByCourseIdAsync(long courseId)
    {
        return await _lessonService.FindLessonsByCourseIdAsync(courseId);
    }

    [HttpPost("create/{courseId:long}")]
    public async Task<IActionResult> CreateLessonAsync(long courseId, [FromBody] LessonRequestDTO lessonRequestDTO)
    {
        var createdLesson = await _lessonService.CreateLessonAsync(courseId, lessonRequestDTO);
        return CreatedAtRoute(nameof(FindLessonByIdAsync), new { lessonId = createdLesson.Id }, createdLesson);
    }

    [HttpPost("create/{courseId:long}/by-user/{userId:long}")]
    public async Task<IActionResult> CreateLessonWithAuthorCheckAsync(long courseId, long userId, [FromBody] LessonRequestDTO lessonRequestDTO)
    {
        var createdLesson = await _lessonService.CreateLessonWithAuthorCheckAsync(courseId, userId, lessonRequestDTO);
        return CreatedAtRoute(nameof(FindLessonByIdAsync), new { lessonId = createdLesson.Id }, createdLesson);
    }


    [HttpDelete("delete/{lessonId:long}")]
    public async Task<IActionResult> DeleteLessonByIdAsync(long lessonId)
    {
        await _lessonService.DeleteLessonByIdAsync(lessonId);
        return NoContent();
    }
}