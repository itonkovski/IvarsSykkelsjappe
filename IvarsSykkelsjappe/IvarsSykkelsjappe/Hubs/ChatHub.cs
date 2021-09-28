using System.Threading.Tasks;
using IvarsSykkelsjappe.Models.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace IvarsSykkelsjappe.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync(
                "NewMessage",
                new Message { User = this.Context.User.Identity.Name, Text = message, });
            //await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
