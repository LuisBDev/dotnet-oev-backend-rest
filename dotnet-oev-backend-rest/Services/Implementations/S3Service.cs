using Amazon.S3;
using Amazon.S3.Model;
using dotnet_oev_backend_rest.Dtos.Response.Record;
using dotnet_oev_backend_rest.Exceptions;
using dotnet_oev_backend_rest.Services.Interfaces;

namespace dotnet_oev_backend_rest.Services.Implementations;

/// <summary>
///     Implementación del servicio para interactuar con Amazon S3.
/// </summary>
public class S3Service : IS3Service
{
    private readonly IAmazonS3 _s3Client;

    public S3Service(IAmazonS3 s3Client)
    {
        _s3Client = s3Client;
    }

    public PresignedUrlDTO GeneratePreSignedUploadUrl(string bucketName, string key, long durationSeconds)
    {
        if (string.IsNullOrEmpty(bucketName) || string.IsNullOrEmpty(key) || durationSeconds <= 0)
            throw new AppException("Los parámetros bucketName, key y durationSeconds son requeridos y deben ser válidos.");

        try
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = bucketName,
                Key = key,
                Verb = HttpVerb.PUT, // Para subir un archivo
                Expires = DateTime.UtcNow.AddSeconds(durationSeconds)
            };

            var url = _s3Client.GetPreSignedURL(request);
            return new PresignedUrlDTO(url);
        }
        catch (AmazonS3Exception e)
        {
            throw new AppException($"Error al generar la URL prefirmada de subida: {e.Message}");
        }
        catch (Exception e)
        {
            throw new AppException($"Error inesperado al generar la URL prefirmada de subida: {e.Message}");
        }
    }

    public PresignedUrlDTO GeneratePreSignedDownloadUrl(string bucketName, string key, long durationSeconds)
    {
        if (string.IsNullOrEmpty(bucketName) || string.IsNullOrEmpty(key) || durationSeconds <= 0)
            throw new AppException("Los parámetros bucketName, key y durationSeconds son requeridos y deben ser válidos.");

        try
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = bucketName,
                Key = key,
                Verb = HttpVerb.GET, // Para descargar un archivo
                Expires = DateTime.UtcNow.AddSeconds(durationSeconds)
            };

            var url = _s3Client.GetPreSignedURL(request);
            return new PresignedUrlDTO(url);
        }
        catch (AmazonS3Exception e)
        {
            throw new AppException($"Error al generar la URL prefirmada de descarga para la clave {key}: {e.Message}");
        }
        catch (Exception e)
        {
            throw new AppException($"Error inesperado al generar la URL prefirmada de descarga para la clave {key}: {e.Message}");
        }
    }
}