using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub
{
    // 用戶加入特定群組
    public async Task JoinGroup(string groupId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        await Clients.Group(groupId).SendAsync("ReceiveSystemMessage", $"{Context.ConnectionId} 加入群組 {groupId}");
    }

    // 用戶離開特定群組（可選）
    public async Task LeaveGroup(string groupId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);
        await Clients.Group(groupId).SendAsync("ReceiveSystemMessage", $"{Context.ConnectionId} 離開群組 {groupId}");
    }

    // 發送訊息給特定群組
    public async Task SendMessage(string groupId, string user, string message)
    {
        await Clients.Group(groupId).SendAsync("ReceiveMessage", groupId, user, message);
    }
}