using dotnet_oev_backend_rest.Dtos.Request;
using dotnet_oev_backend_rest.Dtos.Response;
using dotnet_oev_backend_rest.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_oev_backend_rest.Controllers
{
    [ApiController]
    [Route("api/registration")]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<RegistrationResponseDTO>> CreateRegistration([FromBody] RegistrationRequestDTO registrationRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _registrationService.CreateRegistrationAsync(registrationRequestDTO);
            return CreatedAtAction(nameof(FindRegistrationById), new { id = response.Id }, response);
        }

        [HttpGet("findRegistration/{id}")]
        public async Task<ActionResult<RegistrationResponseDTO>> FindRegistrationById(long id)
        {
            var response = await _registrationService.FindRegistrationByIdAsync(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet("findAllByUserId/{userId}")]
        public async Task<ActionResult<List<RegistrationResponseDTO>>> FindRegistrationsByUserId(long userId)
        {
            var response = await _registrationService.FindRegistrationsByUserIdAsync(userId);
            return Ok(response);
        }

        [HttpGet("findAllByConferenceId/{courseId}")]
        public async Task<ActionResult<List<RegistrationResponseDTO>>> FindRegistrationsByConferenceId(long courseId)
        {
            var response = await _registrationService.FindRegistrationsByConferenceIdAsync(courseId);
            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteRegistrationById(long id)
        {
            await _registrationService.DeleteRegistrationByIdAsync(id);
            return NoContent();
        }

        [HttpGet("findRegisteredUsersByConferenceId/{conferenceId}")]
        public async Task<ActionResult<List<UserResponseDTO>>> FindRegisteredUsersByConferenceId(long conferenceId)
        {
            var response = await _registrationService.FindRegisteredUsersByConferenceIdAsync(conferenceId);
            return Ok(response);
        }
    }
}