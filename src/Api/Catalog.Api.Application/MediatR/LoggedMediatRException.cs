using Catalog.Api.Application.MediatR.Behaviors.Logging;

namespace Catalog.Api.Application.MediatR;

/// <summary>
/// Represents an <see cref="Exception" /> thrown by <see cref="CommandLoggingBehavior{TCommand,TResponse}" /> or <see cref="QueryLoggingBehavior{TQuery,TResponse}" /> after having handled an encountered exception.
/// The purpose of this exception is to tell to the GlobalExceptionHandler middleware that the occurring exception was already logged to avoid duplication.
/// That way the encountered exception is not logged again by the global exception handler as the logging behaviors did that already.
/// This class should not be manually thrown.
/// </summary>
public class LoggedMediatRException : Exception
{
    public LoggedMediatRException()
    {

    }
}
