using dotnet_oev_backend_rest.Dtos.Response.Record;

namespace dotnet_oev_backend_rest.Services.Interfaces;

/// <summary>
///     Interfaz para el servicio que maneja las operaciones con Amazon S3.
/// </summary>
public interface IS3Service
{
    /// <summary>
    ///     Genera una URL prefirmada para subir un archivo a un bucket de S3.
    /// </summary>
    /// <param name="bucketName">El nombre del bucket.</param>
    /// <param name="key">La clave (nombre del archivo) con la que se guardará el objeto.</param>
    /// <param name="durationSeconds">La duración en segundos durante la cual la URL será válida.</param>
    /// <returns>Un DTO que contiene la URL prefirmada para la subida.</returns>
    PresignedUrlDTO GeneratePreSignedUploadUrl(string bucketName, string key, long durationSeconds);

    /// <summary>
    ///     Genera una URL prefirmada para descargar un archivo de un bucket de S3.
    /// </summary>
    /// <param name="bucketName">El nombre del bucket.</param>
    /// <param name="key">La clave (nombre del archivo) del objeto a descargar.</param>
    /// <param name="durationSeconds">La duración en segundos durante la cual la URL será válida.</param>
    /// <returns>Un DTO que contiene la URL prefirmada para la descarga.</returns>
    PresignedUrlDTO GeneratePreSignedDownloadUrl(string bucketName, string key, long durationSeconds);
}