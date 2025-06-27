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

    [HttpPost("create/{courseId:long}")]
    public async Task<ActionResult<LessonResponseDTO>> CreateLessonAsync(long courseId, [FromBody] LessonRequestDTO lessonRequestDTO)
    {
        var createdLesson = await _lessonService.CreateLessonAsync(courseId, lessonRequestDTO);
        // return CreatedAtAction(nameof(CreateLessonAsync), new { id = createdLesson.Id }, createdLesson);
        // solo retorna el created con el objeto creado:
        return Created(string.Empty, createdLesson);
    }
}