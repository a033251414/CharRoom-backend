// using Microsoft.AspNetCore.SignalR;

// public class ChatHub : Hub
// {
//     public async Task SendMessage(string user, string message)
//     {
//         // 廣播訊息給所有連線的用戶
//         await Clients.All.SendAsync("ReceiveMessage", user, message);
//     }
// }
using Microsoft.AspNetCore.SignalR;

namespace Server.Hubs
{
    public class ChatHub : Hub
        {
           // 用戶加入某個群組
    public async Task JoinGroup(string groupId)
        {
           await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
           Console.WriteLine($"{Context.ConnectionId} joined group {groupId}");
        }

    // 用戶離開某個群組（可選）
    public async Task LeaveGroup(string groupId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);
            Console.WriteLine($"{Context.ConnectionId} left group {groupId}");
        }
    // 傳訊息給群組    
    public async Task SendMessage(string groupId, ChatMessage message)
        {
            await Clients.Group(groupId).SendAsync("ReceiveMessage", message);
        }
    }
}
