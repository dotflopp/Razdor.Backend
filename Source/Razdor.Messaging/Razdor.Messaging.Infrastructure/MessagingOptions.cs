namespace Razdor.Messaging.Infrastructure;

public record MessagingOptions(    
    string ConnectionString,
    string DataBaseName
);