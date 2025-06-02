using Microsoft.AspNetCore.Mvc;
using Razdor.Api.Routes.Messaging.ViewModels;
using Razdor.Messaging.Module.Contracts;
using Razdor.Messaging.Module.Services.Commands;
using Razdor.Messaging.Module.Services.Commands.ViewModels;

namespace Razdor.Api.Routes.Messaging;

public static class MessagingRouter
{
    public static IEndpointRouteBuilder MapMessages(this IEndpointRouteBuilder builder)
    {
        IEndpointRouteBuilder api = builder.MapGroup("channel/{channelId:ulong}/messages");
        api.MapPost("/", SendMessageAsync);   
        
        return builder;
    }
    
    private static async Task<IResult> SendMessageAsync(
        [FromRoute] ulong channelId,
        [FromServices] IMessagingModule module,
        [FromBody] MessagePyload messagePyload
    )
    {
         MessageViewModel message = await module.ExecuteCommandAsync(
            new SendMessageCommand(
                channelId, 
                messagePyload.Text, 
                messagePyload.Embed,
                messagePyload.Reference
            )
        );
         
         return Results.Ok(message);
    }
}