using System;
using Microsoft.Extensions.Logging;

namespace AzureBlobStorage.API.Infrastructure.Exceptions;

/// <summary>
/// A generic exception that occurred during service processing; intended to be extended to provide further context
/// about the error(s) that occurred to yield this exception.
/// </summary>
/// <remarks>Not intended to be extended directly, rather one of the subclasses of this generalized definition
/// should be used for more specific error context cases.</remarks>
public abstract class ServiceException : Exception
{
    /// <summary>
    /// The severity with which to treat this error when monitoring the service internally.
    /// </summary>
    public abstract LogLevel Severity { get; }
        
    /// <summary>
    /// Initializes the error without any contextual error message to return to the external client, but another
    /// error that represents a more root cause of the issue.
    /// </summary>
    /// <remarks>Generally used to represent an internal error that won't have useful information to
    /// surface to the client.</remarks>
    /// <param name="innerException"></param>
    protected internal ServiceException(Exception innerException) : base(null, innerException) { }
        
    /// <summary>
    /// Initializes the error with context about what error occurred and how to correct it, if applicable.
    /// </summary>
    /// <remarks>Generally used to surface error information to the calling client.</remarks>
    /// <param name="message"></param>
    protected internal ServiceException(String message) : base(message) { }
        
    /// <summary>
    /// Initializes the error with context about what error occurred and how to correct it, if applicable; includes
    /// another error that represents a more root cause of the issue.
    /// </summary>
    /// <remarks>Generally used to surface error information to the calling client, while allowing for internal
    /// logging and tracking of errors that might require further attention.</remarks>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    protected internal ServiceException(String message, Exception innerException) : base(message, innerException) { }
}