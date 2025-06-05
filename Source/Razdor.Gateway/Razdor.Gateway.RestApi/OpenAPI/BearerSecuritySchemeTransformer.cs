using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace Razdor.RestApi.OpenAPI;

public class BearerSecuritySchemeTransformer: IOpenApiDocumentTransformer
{
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        OpenApiSecurityScheme securityScheme = new();
        securityScheme.Name = "Authorization";
        securityScheme.In = ParameterLocation.Header;
        securityScheme.Type = SecuritySchemeType.ApiKey;
        securityScheme.BearerFormat = "JWT";
        securityScheme.Scheme = "oauth2";
        
        var requirements = new Dictionary<string, OpenApiSecurityScheme>
        {
            [securityScheme.Name] = securityScheme
        };
        
        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes = requirements;

        OpenApiReference reference = new();
        reference.Id = securityScheme.Name;
        reference.Type = ReferenceType.SecurityScheme;
        
        securityScheme = new (securityScheme);
        securityScheme.Reference = reference;
        
        foreach (var operation in document.Paths.Values.SelectMany(path => path.Operations))
        {
            operation.Value.Security.Add(new OpenApiSecurityRequirement {
                [securityScheme] = []
            });
        }
        
        return Task.CompletedTask;
    }
}