namespace dotnet_oev_backend_rest.Exceptions;

public class AppException : Exception
{
    public AppException(string message) : base(message)
    {
    }
}