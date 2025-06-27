using dotnet_oev_backend_rest.Dtos.Request;
using dotnet_oev_backend_rest.Dtos.Response;
using dotnet_oev_backend_rest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_oev_backend_rest.Controllers;

[Route("api/enrollment")]
[ApiController]
public class EnrollmentsController : ControllerBase
{
    private readonly IEnrollmentService _enrollmentService;

    public EnrollmentsController(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    /// <summary>
    ///     Crea una nueva inscripción de un usuario a un curso.
    /// </summary>
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EnrollmentResponseDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateEnrollment([FromBody] EnrollmentRequestDTO enrollmentRequestDTO)
    {
        var newEnrollment = await _enrollmentService.CreateEnrollmentAsync(enrollmentRequestDTO);

        // Retorna 201 Created con la URL para obtener la inscripción recién creada.
        return CreatedAtAction(nameof(FindEnrollmentById), new { id = newEnrollment.Id }, newEnrollment);
    }

    /// <summary>
    ///     Busca una inscripción por su ID.
    /// </summary>
    [HttpGet("findEnrollment/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EnrollmentResponseDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EnrollmentResponseDTO>> FindEnrollmentById(long id)
    {
        var enrollment = await _enrollmentService.FindEnrollmentByIdAsync(id);
        return Ok(enrollment);
    }

    /// <summary>
    ///     Obtiene todas las inscripciones de un usuario específico.
    /// </summary>
    [HttpGet("findAllByUserId/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyList<EnrollmentResponseDTO>))]
    public async Task<ActionResult<IReadOnlyList<EnrollmentResponseDTO>>> FindEnrollmentsByUserId(long userId)
    {
        var enrollments = await _enrollmentService.FindEnrollmentsByUserIdAsync(userId);
        return Ok(enrollments);
    }

    /// <summary>
    ///     Obtiene todas las inscripciones de un curso específico.
    /// </summary>
    [HttpGet("findAllByCourseId/{courseId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyList<EnrollmentResponseDTO>))]
    public async Task<ActionResult<IReadOnlyList<EnrollmentResponseDTO>>> FindEnrollmentsByCourseId(long courseId)
    {
        var enrollments = await _enrollmentService.FindEnrollmentsByCourseIdAsync(courseId);
        return Ok(enrollments);
    }

    /// <summary>
    ///     Obtiene todos los usuarios inscritos en un curso específico.
    /// </summary>
    [HttpGet("findEnrolledUsersByCourseId/{courseId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyList<UserResponseDTO>))]
    public async Task<ActionResult<IReadOnlyList<UserResponseDTO>>> FindEnrolledUsersByCourseId(long courseId)
    {
        var users = await _enrollmentService.FindEnrolledUsersByCourseIdAsync(courseId);
        return Ok(users);
    }


    /// <summary>
    ///     Actualiza una inscripción por su ID (ej. el progreso).
    /// </summary>
    [HttpPatch("update/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EnrollmentResponseDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EnrollmentResponseDTO>> UpdateEnrollmentById(long id, [FromBody] EnrollmentUpdateRequestDTO enrollmentUpdateRequestDTO)
    {
        var updatedEnrollment = await _enrollmentService.UpdateEnrollmentByIdAsync(id, enrollmentUpdateRequestDTO);
        return Ok(updatedEnrollment);
    }
    
    /// <summary>
    ///     Elimina una inscripción por su ID.
    /// </summary>
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEnrollmentById(long id)
    {
        var result = await _enrollmentService.DeleteEnrollmentByIdAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}