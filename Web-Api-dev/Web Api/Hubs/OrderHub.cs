using Domain;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.SignalR;
using Hub = Microsoft.AspNetCore.SignalR.Hub;

namespace Web_Api.Hubs
{
    public class OrderHub :Hub
    {
        public async Task NotifyCreateOrder(int newOrderID)
        {
            await Clients.All.SendAsync("ReceiveNewOrderNotification", newOrderID);
        }
    }
}
