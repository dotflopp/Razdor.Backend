using Razdor.ServiceDefaults;
using Scalar.AspNetCore;
using Razdor.RestApi.ExceptionHandleMiddlewares;
using Razdor.RestApi.Routes;
using Razdor.SignalR;
using Razdor.StartApp;

WebApplicationBuilder builder = WebApplication.CreateSlimBuilder(args);

builder.AddServiceDefaults();
builder.AddAspServices();
builder.AddApplicationServices();

WebApplication app = builder.Build();

app.UseCors();
app.UseStaticFiles("/api");

app.MapOpenApi("/api/swagger/{documentName}/swagger.json");

app.MapScalarApiReference("/api/swagger", options =>
{
    options.WithOpenApiRoutePattern("/api/swagger/{documentName}/swagger.json");
    options.AddDocument("v1", "Main API");
    options.Theme = ScalarTheme.Alternate;
    options.DarkMode = false;
});

app.UseCustomNotAuthorizedResponse();

app.UseAuthentication();
app.UseAuthorization();

app.UseRazdorExceptionHandlerMiddleware();

app.MapSignalRGateway();
app.MapIdentityApi();
app.MapCommunitiesApi();
app.MapMessagingApi();

app.UseNonExistentRouteResponse();

app.Run();