namespace dotnet_oev_backend_rest.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}