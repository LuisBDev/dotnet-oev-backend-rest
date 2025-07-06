using dotnet_oev_backend_rest.Dtos.Response.Record;
using dotnet_oev_backend_rest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_oev_backend_rest.Controllers;

[Route("api/s3")]
[ApiController]
public class S3Controller : ControllerBase
{
    private readonly IS3Service _s3Service;

    public S3Controller(IS3Service s3Service)
    {
        _s3Service = s3Service;
    }


    [HttpGet("file/upload-url")]
    [ProducesResponseType(typeof(PresignedUrlDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GeneratePreSignedUploadUrl([FromQuery] string bucketName, [FromQuery] string key, [FromQuery] long durationSeconds)
    {
        var presignedUrlDto = _s3Service.GeneratePreSignedUploadUrl(bucketName, key, durationSeconds);
        return Ok(presignedUrlDto);
    }


    [HttpGet("file/download-url")]
    [ProducesResponseType(typeof(PresignedUrlDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GeneratePreSignedDownloadUrl([FromQuery] string bucketName, [FromQuery] string key, [FromQuery] long durationSeconds)
    {
        var presignedUrlDto = _s3Service.GeneratePreSignedDownloadUrl(bucketName, key, durationSeconds);
        return Ok(presignedUrlDto);
    }
}