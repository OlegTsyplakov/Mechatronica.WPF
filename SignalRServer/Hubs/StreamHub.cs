﻿using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;


namespace SignalRServer.Hubs
{
    public class StreamHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("--> Connection Opened: " + Context.ConnectionId);
            Clients.Client(Context.ConnectionId).SendAsync("ReceiveConnID", Context.ConnectionId);
            return base.OnConnectedAsync();
        }


        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine("--> Connection Closed: " + Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }


        public async Task SendMessageAsync(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
            //var routeOb = JsonConvert.DeserializeObject<dynamic>(message);
            //Console.WriteLine("To: " + routeOb.To.ToString());
            //Console.WriteLine("Message Recieved on: " + Context.ConnectionId);
            //if (routeOb.To.ToString() == string.Empty)
            //{
            //    Console.WriteLine("Broadcast");
            //    await Clients.All.SendAsync("ReceiveMessage", message);
            //}
            //else
            //{
            //    string toClient = routeOb.To;
            //    Console.WriteLine("Targeted on: " + toClient);

            //    await Clients.Client(toClient).SendAsync("ReceiveMessage", message);

            //}
        }
    }
}

