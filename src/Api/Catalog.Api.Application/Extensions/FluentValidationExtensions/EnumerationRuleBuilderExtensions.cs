using Ardalis.SmartEnum;
using FluentValidation;

namespace Catalog.Api.Application.Extensions.FluentValidationExtensions;

public static class EnumerationRuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, int> EnumerationExists<T, TSmartEnum>(this IRuleBuilder<T, int> ruleBuilder)
        where TSmartEnum : SmartEnum<TSmartEnum>
    {
        return ruleBuilder.Must(enumerationId => SmartEnum<TSmartEnum>.List.Any(enumeration => enumeration.Value == enumerationId));
    }

    /// <summary>
    /// Rule that checks if a given of list of int are existing values of a given enumeration.
    /// Every values must exist and a empty or null list of integer will be accepted.
    /// </summary>
    /// <typeparam name="T">The type on which is apply the date.</typeparam>
    /// <typeparam name="TSmartEnum">The type of the enumeration that will be checked.</typeparam>
    /// <param name="ruleBuilder">The source <see cref="IRuleBuilder{T,TProperty}"/>.</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, IEnumerable<int>?> AllEnumerationExists<T, TSmartEnum>(this IRuleBuilder<T, IEnumerable<int>?> ruleBuilder)
        where TSmartEnum : SmartEnum<TSmartEnum>
    {
        return ruleBuilder
            .Must(enumerationIdsToCheck =>
            {
                return enumerationIdsToCheck?.All(enumeration => SmartEnum<TSmartEnum>.TryFromValue(enumeration, out _)) ?? true;
            })
            .WithMessage((_, ids) =>
            {
                var existing    = SmartEnum<TSmartEnum>.List.Select(@enum => @enum.Value);
                var nonExisting = ids!.Except(existing);
                return $"Following ids doesn't not exists: [{string.Join(", ", nonExisting.Select(n => n))}]";
            })
            .WithState((_, ids) => ids);
    }
}
