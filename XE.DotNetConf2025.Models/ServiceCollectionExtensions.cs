namespace XE.DotNetConf2025.Models;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for registering validation services for model types in an <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds validation services for types defined in the Models namespace to the specified service collection.
    /// </summary>
    /// <remarks>This extension method is intended to simplify the registration of validation services for
    /// model types. It should be called during application startup as part of service configuration.</remarks>
    /// <param name="collection">The service collection to which validation services will be added.</param>
    /// <returns>The same service collection instance, with validation services for model types registered.</returns>
    public static IServiceCollection AddValidationForTypesInModels(this IServiceCollection collection)
    {
        return collection.AddValidation();
    }
}
