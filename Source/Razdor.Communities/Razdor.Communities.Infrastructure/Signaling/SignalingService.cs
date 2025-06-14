using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Razdor.Communities.Module.Services.Channels.Commands;
using Razdor.Shared.Module.Serialization;

namespace Razdor.Communities.Infrastructure.Signaling;

public record SignalingServerRequest(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong ChannelId,
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong UserId
);

public class SignalingService(IOptions<JsonOptions> jsonOptions): ISignalingService
{
    private JsonSerializerOptions serializerOptions = jsonOptions.Value.SerializerOptions;
    private HttpClient http = new HttpClient()
    {
        BaseAddress = new Uri("http://localhost:8070/")
    };

    [RequiresUnreferencedCode("JsonContent.Create требует неуправляемый код")]
    public async Task<SessionViewModel> CreateUserSession(ulong channelId, ulong userId)
    {
        HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "/sessions/");
        message.Content = JsonContent.Create(
            new SignalingServerRequest(channelId, userId), 
            options: serializerOptions
        );
        
        HttpResponseMessage response = await http.SendAsync(message);
        
        return JsonSerializer.Deserialize<SessionViewModel>(
            await response.Content.ReadAsStreamAsync(), 
            options: serializerOptions
        ) ?? throw new InvalidOperationException("Invalid sessions response");
    }
}