using Microsoft.AspNetCore.Mvc;
using Razdor.Messages.Module.Contracts;
using Razdor.Messages.Module.Services.Commands;
using Razdor.Messages.Module.Services.Commands.ViewModels;
using Razdor.Messages.Module.Services.Query;
using Razdor.RestApi.Multipart;
using Razdor.RestApi.Routes.Messages.ViewModels;
using Razdor.Shared.Module.Media;

namespace Razdor.RestApi.Routes.Messages;

public static class MessagingRouter
{
    public static IEndpointRouteBuilder MapMessages(this IEndpointRouteBuilder builder)
    {
        IEndpointRouteBuilder api = builder.MapGroup("/channels/{channelId:ulong}/messages");
        
        api.MapPost("/", SendMessageAsync)
            .DisableAntiforgery()
            .WithSummary("Отправить сообщение в канал");
        api.MapGet("/", GetMessagesAsync)
            .WithSummary("Получить сообщения канала");

        builder.MapGet("/attachments/{channelId:ulong}/{messageId:ulong}/{attachmentId:ulong}", GetAttachmentsAsync)
            .WithSummary("Получить файл вложения");

        return builder;
    }
    private static async Task<IResult> GetAttachmentsAsync(
        [FromServices] IMessagesModule module,
        [FromRoute] ulong channelId,
        [FromRoute] ulong messageId,
        [FromRoute] ulong attachmentId
    )
    {
        MediaFile media = await module.ExecuteQueryAsync(new GetAttachmentQuery(channelId, messageId, attachmentId));
        return Results.File(media.Stream, media.ContentType, media.FileName);
    }
    
    private static async Task<IResult> GetMessagesAsync(
        [FromServices] IMessagesModule module,
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
        [FromServices] IMessagesModule module,
        [FromRoute] ulong channelId,
        [FromServices] ContentWithFilesAccessor contentWithFilesAccessor
    )
    {
        ContentWithFiles<MessagePyload> contentWithFiles = await contentWithFilesAccessor.ParseAsync<MessagePyload>();
        MessageViewModel message = await module.ExecuteCommandAsync(
            new SendMessageCommand(
                channelId,
                contentWithFiles.Conetent.Text,
                contentWithFiles.Conetent.Embed,
                contentWithFiles.Conetent.Reference,
                contentWithFiles.Files.Select(x => new MediaFile(
                    x.Filename, x.MediaType, x.Stream
                ))
            )
        );

        return Results.Ok(message);
    }
}