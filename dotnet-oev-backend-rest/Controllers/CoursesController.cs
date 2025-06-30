using dotnet_oev_backend_rest.Dtos.Request;
using dotnet_oev_backend_rest.Dtos.Response;
using dotnet_oev_backend_rest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_oev_backend_rest.Controllers;

[ApiController]
[Route("api/course")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    /// <summary>
    ///     Crea un nuevo curso asociado a un usuario.
    /// </summary>
    [HttpPost("create/{userId}")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CourseResponseDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateCourse(long userId, [FromBody] CourseRequestDTO courseRequestDTO)
    {
        var newCourse = await _courseService.CreateCourseAsync(userId, courseRequestDTO);

        // Retorna un 201 Created con la ubicación del nuevo recurso en el header "Location".
        return CreatedAtAction(nameof(FindCourseById), new { id = newCourse.Id }, newCourse);
    }

    /// <summary>
    ///     Obtiene una lista de todos los cursos.
    /// </summary>
    [HttpGet("findAll")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CourseResponseDTO>))]
    public async Task<ActionResult<IReadOnlyList<CourseResponseDTO>>> FindAll()
    {
        var courses = await _courseService.FindAllCoursesAsync();
        return Ok(courses);
    }

    /// <summary>
    ///     Obtiene todos los cursos publicados por un usuario específico.
    /// </summary>
    [HttpGet("findAllByUserId/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CourseResponseDTO>))]
    public async Task<ActionResult<IReadOnlyList<CourseResponseDTO>>> FindAllByUserId(long userId)
    {
        var courses = await _courseService.FindAllCoursesByUserIdAsync(userId);
        return Ok(courses);
    }

    /// <summary>
    ///     Obtiene los detalles de un curso específico por su ID.
    /// </summary>
    /// <param name="id">ID del curso a consultar (debe ser mayor que 0)</param>
    /// <returns>Los detalles del curso si existe</returns>
    [HttpGet("findCourse/{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CourseResponseDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CourseResponseDTO>> FindCourseById(long id)
    {
        if (id <= 0)
            return BadRequest("El ID del curso debe ser un número mayor que cero.");

        var course = await _courseService.FindCourseByIdAsync(id);

        return Ok(course);
    }

    /// <summary>
    ///     Elimina un curso por su ID.
    /// </summary>
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCourseById(long id)
    {
        var result = await _courseService.DeleteCourseByIdAsync(id);
        if (!result)
            // Si el servicio indica que el curso no se encontró, devolvemos 404.
            return NotFound();

        // Retorna 204 No Content si la eliminación fue exitosa.
        return NoContent();
    }

    /// <summary>
    ///     Actualiza parcialmente un curso por su ID.
    /// </summary>
    [HttpPatch("update/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CourseResponseDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CourseResponseDTO>> UpdateCourseById(long id,
        [FromBody] UpdateCourseRequestDTO courseRequestDTO)
    {
        var updatedCourse = await _courseService.UpdateCourseByIdAsync(id, courseRequestDTO);
        // La excepción NotFoundException será manejada por el middleware.
        return Ok(updatedCourse);
    }

    [HttpPatch("update/{id}/by-user/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CourseResponseDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<CourseResponseDTO>> UpdateCourseWithAuthorCheck(long id, long userId, [FromBody] UpdateCourseRequestDTO courseRequestDTO)
    {
        var updatedCourse = await _courseService.UpdateCourseWithAuthorCheckAsync(id, userId, courseRequestDTO);
        return Ok(updatedCourse);
    }
}