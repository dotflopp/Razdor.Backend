using Microsoft.AspNetCore.Mvc;
using Razdor.Api.Middlewares.ViewModels;
using Razdor.Api.Multipart;
using Razdor.Api.Routes.Messaging.ViewModels;
using Razdor.Messages.Module.Contracts;
using Razdor.Messages.Module.Services.Commands;
using Razdor.Messages.Module.Services.Commands.ViewModels;
using Razdor.Messages.Module.Services.Query;

namespace Razdor.Api.Routes.Messaging;

public static class MessagingRouter
{
    public static IEndpointRouteBuilder MapMessages(this IEndpointRouteBuilder builder)
    {
        IEndpointRouteBuilder api = builder.MapGroup("channel/{channelId:ulong}/messages");
        api.MapPost("/", SendMessageAsync)
            .DisableAntiforgery()
            .WithSummary("Отправить сообщение в канал");
        api.MapGet("/", GetMessagesAsync)
            .WithSummary("Получить сообщения канала");

        return builder;
    }
    private static async Task<IResult> GetMessagesAsync(
        [FromServices] IMessagingModule module,
        [FromRoute] ulong channelId,
        [FromQuery] ulong? lastMessageId,
        [FromQuery] int? messagesCount
    )
    {
        IEnumerable<MessageViewModel> messags = await module.ExecuteQueryAsync(
            new GetMessagesQuery(channelId, lastMessageId, messagesCount)
        );
        
        return Results.Ok(messags);
    }

    // private static async Task<IResult> SendMessageAsync(
    //     [FromServices] IMessagingModule module,
    //     [FromRoute] ulong channelId,
    //     [FromBody] MessagePyload messagePyload
    // )
    // {
    //      MessageViewModel message = await module.ExecuteCommandAsync(
    //         new SendMessageCommand(
    //             channelId, 
    //             messagePyload.Text, 
    //             messagePyload.Embed,
    //             messagePyload.Reference
    //         )
    //      );
    //      
    //      return Results.Ok(message);
    // }

    private static async Task<IResult> SendMessageAsync(
        [FromServices] IMessagingModule module,
        [FromRoute] ulong channelId,
        [FromServices] ContentWithFilesAccessor<MessagePyload> contentWithFilesAccessor
    )
    {
        ContentWithFiles<MessagePyload> contentWithFiles = await contentWithFilesAccessor.ParseAsync();
        MessageViewModel message = await module.ExecuteCommandAsync(
            new SendMessageCommand(
                channelId,
                contentWithFiles.Conetent.Text,
                contentWithFiles.Conetent.Embed,
                contentWithFiles.Conetent.Reference,
                contentWithFiles.Files.Select(x => new AttachmentFileViewModel(
                    x.Name, x.Filename, x.MediaType, x.Stream
                ))
            )
        );

        return Results.Ok(message);
    }
}