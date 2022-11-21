namespace AzureBlobStorage.API.Infrastructure.Exceptions;

public class ContainerAlreadyExistsException : ResourceNotFoundServiceException
{
    public ContainerAlreadyExistsException(string containerName) : base($"Unable to create container '{containerName}' as it already exists") { }
}
