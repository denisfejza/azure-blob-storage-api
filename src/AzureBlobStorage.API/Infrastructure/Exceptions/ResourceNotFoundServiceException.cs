namespace AzureBlobStorage.API.Infrastructure.Exceptions;

/**
 * Error representing a failed attempt to retrieve a requested resource for further operation; may represent that
 * the resource is actually missing, or that the client does not have adequate access to the resource that does
 * exist.
 */
public class ResourceNotFoundServiceException : ServiceException
{
    public override LogLevel Severity => LogLevel.Information;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="innerException"></param>
    protected ResourceNotFoundServiceException(Exception innerException) : base(innerException) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    protected ResourceNotFoundServiceException(String message) : base(message) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    protected ResourceNotFoundServiceException(
        String message,
        Exception innerException
    ) : base(message, innerException) { }
}