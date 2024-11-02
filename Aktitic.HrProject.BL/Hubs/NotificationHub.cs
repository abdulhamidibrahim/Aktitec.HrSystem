using Microsoft.AspNetCore.SignalR;

namespace Aktitic.HrProject.BL.SignalR;

public class NotificationHub : Hub
{
    public async Task SendNotification(string user, string message,string groupName) 
        => await Clients.Group(groupName).SendAsync("ReceiveNotification", user, message);
    public async Task AddToGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined the company {groupName}.");
    }

    // Method to remove a user from a group
    public async Task RemoveFromGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} has left the company {groupName}.");
    }

}