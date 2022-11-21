using System;
using Microsoft.Extensions.Logging;

namespace AzureBlobStorage.API.Infrastructure.Exceptions;

public class InvalidRequestException : ServiceException
{
    public override LogLevel Severity => LogLevel.Information;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="innerException"></param>
    protected InvalidRequestException(Exception innerException) : base(innerException) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    protected InvalidRequestException(String message) : base(message) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    protected InvalidRequestException(
        String message,
        Exception innerException
    ) : base(message, innerException) { }
}