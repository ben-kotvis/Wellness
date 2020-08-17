using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Api
{
    public class NotificationHub : Hub, IClientNotifier
    {        
        public async Task SendNotification(string message)
        {
            await Clients.All.SendAsync("NotificationMessage", message);
        }
    }

    public class ClientNotificationHub : IClientNotifier
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public ClientNotificationHub(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task SendNotification(string message)
        {
            await _hubContext.Clients.All.SendAsync("NotificationMessage", message);
        }
    }
}
