using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;
using Razdor.Shared.Module;
using Razdor.Shared.Module.Serialization;

namespace Razdor.Api.OpenAPI;

public class StringTypesSchemaFilter: IOpenApiSchemaTransformer
{
    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
    {
        if (context.JsonPropertyInfo?.AttributeProvider == null)
            return Task.CompletedTask;

        ICustomAttributeProvider attributes = context.JsonPropertyInfo.AttributeProvider;
        
        bool hasJsonStringConverter = attributes
            .GetCustomAttributes(typeof(JsonConverterAttribute),true)
            .Any(HasJsonStringConverterTypeInConstructor);
        
        if (hasJsonStringConverter)
            schema.Type = "string";
        
        return Task.CompletedTask;
    }

    private bool HasJsonStringConverterTypeInConstructor(object arg)
    {
        if (arg is not JsonConverterAttribute attribute)
            return false;
            
        return attribute.ConverterType == typeof(JsonStringULongConverter);
    }
    
}