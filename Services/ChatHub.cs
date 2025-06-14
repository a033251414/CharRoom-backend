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
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
