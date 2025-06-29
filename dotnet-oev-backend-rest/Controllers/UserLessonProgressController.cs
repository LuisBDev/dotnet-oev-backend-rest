using dotnet_oev_backend_rest.Dtos.Response;
using dotnet_oev_backend_rest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_oev_backend_rest.Controllers;

[ApiController]
[Route("api/user-lesson-progress")]
public class UserLessonProgressController : ControllerBase
{
    private readonly IUserLessonProgressService _userLessonProgressService;

    public UserLessonProgressController(IUserLessonProgressService userLessonProgressService)
    {
        _userLessonProgressService = userLessonProgressService;
    }

    [HttpGet("user/{userId:long}/course/{courseId:long}")]
    public async Task<ActionResult<IReadOnlyList<UserLessonProgressResponseDTO>>> FindUserLessonProgressesByUserIdAndCourseIdAsync(long userId, long courseId)
    {
        var progresses = await _userLessonProgressService.FindUserLessonProgressesByUserIdAndCourseIdAsync(userId, courseId);
        return Ok(progresses);
    }

    [HttpPost("mark-completed/user/{userId:long}/lesson/{lessonId:long}")]
    public async Task<IActionResult> MarkLessonAsCompletedAsync(long userId, long lessonId)
    {
        await _userLessonProgressService.MarkLessonAsCompletedAsync(userId, lessonId);
        return NoContent();
    }

    [HttpPost("mark-not-completed/user/{userId:long}/lesson/{lessonId:long}")]
    public async Task<IActionResult> MarkLessonAsNotCompletedAsync(long userId, long lessonId)
    {
        await _userLessonProgressService.MarkLessonAsNotCompletedAsync(userId, lessonId);
        return NoContent();
    }
}