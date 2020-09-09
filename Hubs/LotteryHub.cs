using System.Threading.Tasks;
using Lottery.Models.Attendee;
using Microsoft.AspNetCore.SignalR;

namespace Lottery.Hubs
{
    public class LotteryHub : Hub
    {
        public async Task SendMessage(AttendeeViewModel attendee)
        {
            await Clients.All.SendAsync("messageReceived", attendee);
        }
    }
}