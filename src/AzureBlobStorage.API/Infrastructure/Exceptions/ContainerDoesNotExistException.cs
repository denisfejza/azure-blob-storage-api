namespace AzureBlobStorage.API.Infrastructure.Exceptions;

public class ContainerDoesNotExistException : InvalidRequestException
{
    public ContainerDoesNotExistException(string containerName) : base($"'{containerName}' does not exist.") { }
}
