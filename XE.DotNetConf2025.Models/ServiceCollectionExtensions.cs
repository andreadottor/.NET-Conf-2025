namespace XE.DotNetConf2025.Models;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddValidationForTypesInModels(this IServiceCollection collection)
    {
        return collection.AddValidation();
    }
}
