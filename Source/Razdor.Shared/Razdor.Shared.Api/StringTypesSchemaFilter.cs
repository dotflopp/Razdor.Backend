using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Razdor.Shared.Module;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Razdor.Shared.Api;

public class StringTypesSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.MemberInfo?.CustomAttributes is null)
            return;

        bool changeTypeToString = context.MemberInfo.CustomAttributes.Any(HasJsonStringConverterTypeInConstructor);

        if (changeTypeToString)
            schema.Type = "string";
    }

    private bool HasJsonStringConverterTypeInConstructor(CustomAttributeData arg)
    {
        if (arg.AttributeType != typeof(JsonConverterAttribute))
            return false;

        return arg.ConstructorArguments.Any(
            IsJsonStringConverterType
        );
    }

    private bool IsJsonStringConverterType(CustomAttributeTypedArgument arg)
    {
        if (arg.Value is not Type type)
            return false;

        return type == typeof(JsonStringEnumConverter) || type == typeof(JsonStringULongConverter);
    }
}