using Microsoft.AspNetCore.SignalR;

namespace Aktitic.HrProject.BL.SignalR;

public class NotificationHub : Hub
{
    public async Task SendNotification(string user, string message) 
        => await Clients.All.SendAsync("ReceiveMessage", user, message);
}