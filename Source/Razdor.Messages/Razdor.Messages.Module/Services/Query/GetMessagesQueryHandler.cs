using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Messages.Domain;
using Razdor.Messages.Module.DataAccess;
using Razdor.Messages.PublicEvents.ViewModels;

namespace Razdor.Messages.Module.Services.Query;

public class GetMessagesQueryHandler(
    MessagesDbContext context
): IQueryHandler<GetMessagesQuery, IEnumerable<MessageViewModel>> 
{
    public async ValueTask<IEnumerable<MessageViewModel>> Handle(GetMessagesQuery query, CancellationToken cancellationToken)
    {
        int messagesCount = query.MessagesCount ?? GetMessagesQuery.DefaultMessageCount;
        
        if (messagesCount < 0) 
            messagesCount = GetMessagesQuery.DefaultMessageCount;

        List<Message> messages;
        if (query.LastMessageId is not null)
        {
            messages = await context.Messages
                .AsNoTracking()
                .Where(x => x.Id < query.LastMessageId && x.ChannelId == query.ChannelId)
                .OrderByDescending(x => x.Id)
                .Take(messagesCount)
                .ToListAsync(cancellationToken);
        }
        else
        {
            messages = await context.Messages
                .AsNoTracking()
                .Where(x => x.ChannelId == query.ChannelId)
                .OrderByDescending(x => x.Id)
                .Take(messagesCount)
                .ToListAsync(cancellationToken);
        }

        return messages.Select(MessageViewModel.From);
    }
}