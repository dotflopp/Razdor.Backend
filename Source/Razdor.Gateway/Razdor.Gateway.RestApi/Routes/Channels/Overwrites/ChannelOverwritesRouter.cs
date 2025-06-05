using Microsoft.AspNetCore.Mvc;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.Module.Services.Channels.Commands;
using Razdor.Communities.Module.Services.Channels.ViewModels;
using Razdor.RestApi.Routes.Channels.Overwrites.ViewModels;

namespace Razdor.RestApi.Routes.Channels.Overwrites;

public static class ChannelOverwritesRouter
{
    public static IEndpointRouteBuilder MapChannelOverwrites(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder api = builder.MapGroup("/channels/{channelId:ulong}/overwrites/")
            .WithTags("Channels");

        api.MapPatch("/{overwriteId}", AddChannelOverwriteAsync)
            .WithSummary("Добавить или изменить перезапись прав");

        return builder;
    }
    private static async Task AddChannelOverwriteAsync(
        [FromServices] ICommunitiesModule module,
        [FromRoute] ulong channelId,
        [FromRoute] ulong overwriteId,
        [FromBody] OverwritePyload pyload
    )
    {
        await module.ExecuteCommandAsync(
            new AddOverwriteCommand(
                channelId,
                new OverwriteViewModel(
                    overwriteId,
                    pyload.TargetType,
                    pyload.Permissions
                )
            )
        );
    }
}