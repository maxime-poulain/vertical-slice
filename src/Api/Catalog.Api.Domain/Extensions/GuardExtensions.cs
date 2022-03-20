using Ardalis.GuardClauses;

namespace Catalog.Api.Domain.Extensions;

public static class GuardExtensions
{
    /// <summary>
    /// Ensures that <see cref="string" />'s length is equal or greater than a certain length.
    /// </summary>
    /// <param name="guardClause"><see cref="IGuardClause" /> on which this new guard is added to.</param>
    /// <param name="input">The string whose length will be asserted.</param>
    /// <param name="minLength">The minimum number of characters that <paramref name="input "/> can have.</param>
    /// <param name="parameterName"><paramref name="input" />'s argument name of the caller that was passed to the method.</param>
    /// <param name="message">Optional message of the exception thrown if the assertion fails.</param>
    /// <returns><paramref name="input" /></returns>
    /// <exception cref="ArgumentException">The parameter <paramref name="input" />'s length is lower than <paramref name="minLength" /></exception>
    public static string MinLength(this IGuardClause guardClause, string? input, int minLength, string parameterName, string? message = null)
    {
        message ??= $"{parameterName} must be {minLength} characters long";
        return guardClause.Ensure(input, str => str is not null && str.Length >= minLength, message)!;
    }

    /// <summary>
    /// Ensures that <see cref="string" />'s length is lower or equal to a certain length.
    /// </summary>
    /// <param name="guardClause"><see cref="IGuardClause" /> on which this new guard is added to.</param>
    /// <param name="input">The string whose length will be asserted.</param>
    /// <param name="maxLength">The maximum number of characters that <paramref name="input "/> can have.</param>
    /// <param name="parameterName"><paramref name="input" />'s argument name of the caller that was passed to the method.</param>
    /// <param name="message">Optional message of the exception thrown if the assertion fails.</param>
    /// <returns><paramref name="input" /></returns>
    /// <exception cref="ArgumentException">The parameter <paramref name="input" />'s length is greater than <paramref name="maxLength" />.</exception>
    public static string MaxLength(this IGuardClause guardClause, string? input, int maxLength, string parameterName, string? message = null)
    {
        message ??= $"{parameterName} must not exceed {maxLength} characters long";
        return guardClause.Ensure(input, str => str is not null && str.Length <= maxLength, message)!;
    }

    /// <summary>
    /// Ensures that a <see cref="string" />'s length is between a range of number.
    /// </summary>
    /// <param name="guardClause"><see cref="IGuardClause" /> on which this new guard is added to.</param>
    /// <param name="input">The string whose length will be asserted.</param>
    /// <param name="minLength">The minimum number of characters that <paramref name="input "/> can have.</param>
    /// <param name="maxLength">The maximum number of characters that <paramref name="input "/> can have.</param>
    /// <param name="parameterName"><paramref name="input" />'s argument name of the caller that was passed to the method.</param>
    /// <param name="message">Optional message of the exception thrown if the assertion fails.</param>
    /// <returns><paramref name="input"/></returns>
    /// <exception cref="ArgumentException">The parameter <paramref name="input" />'s length is not in the range (<param name="minLength"></param>, <paramref name="maxLength"/>).</exception>
    public static string Between(this IGuardClause guardClause, string? input, int minLength, int maxLength, string parameterName, string? message = null)
    {
        Guard.Against.MinLength(input, minLength, parameterName, message);
        return Guard.Against.MaxLength(input, maxLength, parameterName, message);
    }

    /// <summary>
    /// Ensures a given predicate evaluates to true.
    /// </summary>
    /// <typeparam name="T">The type of <paramref name="input" />.</typeparam>
    /// <param name="guardClause"><see cref="IGuardClause" /> on which this new guard is added to.</param>
    /// <param name="input">Source object on which the assertion is made.</param>
    /// <param name="predicate">The predicate that determine if the assertion is valid or not.</param>
    /// <param name="message">Message passed to the exception raised in case of negative evaluation of <paramref name="predicate"/>.</param>
    /// <returns><paramref name="input" /></returns>
    /// <exception cref="ArgumentException">The <paramref name="predicate"/> evaluates to false.</exception>
    public static T? Ensure<T>(this IGuardClause guardClause, T? input, Func<T?, bool> predicate, string message) where T : notnull
    {
        if (!predicate(input))
        {
            throw new ArgumentException(message);
        }

        return input;
    }
}
