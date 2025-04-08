using Microsoft.AspNetCore.SignalR;
using Razdor.Signaling.Internal;

namespace Razdor.Signaling.Routing.Signaling
{
    public class SignalingHub : Hub
    {
        private ILogger<SignalingHub> _logger;

        private ISignalingInternalService _signalingService;
        private IDictionary<string, IRoom> _usersRooms;
        private IDictionary<string, string> _sessionConnections;
        
        public SignalingHub(
            ILogger<SignalingHub> logger,
            ISignalingInternalService signalingService
        ){
            _logger = logger;
            _signalingService = signalingService;
            _sessionConnections = signalingService.SessionConnections;
            _usersRooms = signalingService.UsersRooms;
        }

        public override Task OnConnectedAsync()
        {
            _logger.LogInformation("Connected");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _logger.LogInformation("Disconnected {0}", exception);
            var session = _sessionConnections.FirstOrDefault(x => x.Value == Context.ConnectionId);
            if (session.Value == null)
                return;
            
            await DisconnectAsync(session.Key);
            await base.OnDisconnectedAsync(exception);
        }

        [HubMethodName("Connect")]
        public async Task ConnectAsync(string sessionId, UserIdentity user)
        {
            _logger.LogInformation("Connecting to session {0}", sessionId);
            IRoom? room = await _signalingService.FindRoomBySessionAsync(sessionId);
            
            if (room == null)
                throw new Exception($"Room {sessionId} not found");
            
            _sessionConnections[sessionId] = Context.ConnectionId;
            await room.ConnectUserAsync(sessionId, user);
            await SendToRoomBySessions(room, "UserConnected", user, sessionId);
        }
        
        [HubMethodName("Disconnect")]
        public async Task DisconnectAsync(string sessionId)
        {
            _logger.LogInformation("Disconnect from session {0}", sessionId);
            IRoom? room = await _signalingService.FindRoomBySessionAsync(sessionId);
            if (room == null)
                throw new Exception($"Room {sessionId} not found");
            
            _sessionConnections.Remove(sessionId);
            await room.DisconnectUserAsync(sessionId);
            
            UserIdentity? user = await room.FindUserAsync(sessionId);
            if (user != null)
            {
                await SendToRoomBySessions(room, "UserDisconnected", user, sessionId);
            }
        }
        
        [HubMethodName("Offer")]
        public async Task OfferAsync(string sessionId, object offer)
        {
            _logger.LogInformation("Offer {}", sessionId);
            
            IRoom? room = await _signalingService.FindRoomBySessionAsync(sessionId);
            
            if (room == null)
                return;
            
            await SendToRoomBySessions(room, "Offer", new OfferData(sessionId, offer), sessionId);
        }
        
        [HubMethodName("Answer")]
        public async Task AnswerAsync(string sessionId, string toId, object answer)
        {
            _logger.LogInformation("Answer {}", sessionId);
         
            if (!_sessionConnections.TryGetValue(toId, out string? connectionId))
                return;
            
            await Clients.Client(connectionId).SendAsync("Answer", answer);
        }

        [HubMethodName("IceCandidate")]
        public async Task IceCandidateAsync(string sessionId, object ice)
        {
            _logger.LogInformation("IceCandidate {}", sessionId);
            
            IRoom? room = await _signalingService.FindRoomBySessionAsync(sessionId);
            
            if (room == null)
                return;
         
            await SendToRoomBySessions(room, "IceCandidate", ice);
        }

        
        public async Task SendToRoomBySessions(IRoom room, string method, object arg1, string? ignoredSession = null)
        {
            List<Task> tasks = new();
            
            foreach (var session in await room.GetSessionsAsync())
            {
                if (session == ignoredSession)
                    continue;
                
                if (!_sessionConnections.TryGetValue(session, out string? connection))
                    continue;
                
                Task task = Clients.Client(connection).SendAsync(method, arg1);
                tasks.Add(task);
            }

            try
            {
                await Task.WhenAll(tasks);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }

    public class OfferData
    {
        public OfferData(string from, object offer)
        {
            From = from;
            Offer = offer;
        }

        public object Offer { get; }
        public string From { get; }
    }
}
