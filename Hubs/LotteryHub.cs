using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Lottery.Hubs
{
    public class LotteryHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("messageReceived", message);
        }
    }
}