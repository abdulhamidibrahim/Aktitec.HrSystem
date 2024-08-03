using Aktitic.HrProject.BL.Utilities;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.UnitOfWork;
using Microsoft.AspNetCore.SignalR;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL.SignalR;

public class ChatHub : Hub
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserUtility _userUtility;

    public ChatHub(IUnitOfWork unitOfWork,UserUtility userUtility)
    {
        _unitOfWork = unitOfWork;
        _userUtility = userUtility;
    }
    // public async Task SendMessage(string user, string message) 
    //     => await Clients.All.SendAsync("ReceiveMessage", user, message);
    // public async Task SendPrivateMessage(string user, string message) 
    //     => await Clients.User(user).SendAsync("ReceiveMessage", user, message);
    //
    // public async Task SendGroupMessage(string groupName, string message) 
    //     => await Clients.Group(groupName).SendAsync("ReceiveMessage", message);


    public override Task OnConnectedAsync()
    {
        var userId = _userUtility.GetUserId(); // Assuming UserIdentifier is set up
        _unitOfWork.ApplicationUser.AddConnection(userId, Context.ConnectionId);
        return base.OnConnectedAsync();
    }
    
    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _unitOfWork.ApplicationUser.RemoveConnection(Context.ConnectionId);
        // return base.OnDisconnectedAsync(exception);
        return Task.CompletedTask;
    }
    
    
    // public async Task SendMessage(string user, string message)
    // {
    //     await Clients.All.SendAsync("ReceiveMessage", user, message);
    // }

    public async Task JoinCompanyGroup(int companyId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, companyId.ToString());
    }

    public async Task LeaveCompanyGroup(int companyId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, companyId.ToString());
    }
    public async Task AddUsersToGroup(int adminId, List<int> userId, int companyId)
    {
        if (!_unitOfWork.ApplicationUser.IsAdmin(adminId))
        {
            throw new HubException("Only admins can add users to groups.");
        }

        foreach (var id in userId)
        {
            var user = _unitOfWork.ApplicationUser.GetById(id);
            if (user == null)
            {
                throw new HubException("User not found.");
                
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, companyId.ToString());
            await Clients.Group(companyId.ToString()).SendAsync("UserAddedToGroup", user.UserName);
        }
       
    }
}