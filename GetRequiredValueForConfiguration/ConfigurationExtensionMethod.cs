    using System;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Configuration;

public static class ConfigurationExtensionMethod
{
    public static string GetRequiredNoEmptyValue(this IConfiguration config, string jsonPath)
    {
        var value = config.GetValue<string>(jsonPath);

        if (value == null || string.IsNullOrEmpty(value))
        {
            throw new InvalidOperationException($"Configuration value for '{jsonPath}' is required but not found or empty in the configuration.");
        }

        return value;
    }


    public static T GetRequiredValue<T>(this IConfiguration config, string jsonPath)
    {
        var value = config.GetValue<T>(jsonPath);

        if (value == null || value.Equals(default(T)))
        {
            throw new InvalidOperationException($"Configuration value for '{jsonPath}' is required but not found in the configuration.");
        }

        return value;
    }
}

