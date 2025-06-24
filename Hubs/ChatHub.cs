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
        //更新收回訊息
        public async Task RecallMessage(string groupId, string messageId)
        {
            Console.WriteLine($"[RecallMessage] Group:{groupId}, Message:{messageId}");
            await Clients.Group(groupId).SendAsync("ReceiveRecalledMessage", messageId);
          
        }
    }
    
    
}
