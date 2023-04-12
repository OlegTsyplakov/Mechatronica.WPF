using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    public class SignalRConnection
    {
        public async void Start()
        {
            var url = "http://localhost:58561/streamHub";

            var connection = new HubConnectionBuilder()
                .WithUrl(url)
                .WithAutomaticReconnect()
                .Build();
            await connection.StartAsync();
            connection.On<string>("ReceiveMessage", (message) => OnReceiveMessage(message));

        }

        private void OnReceiveMessage( string message)
        {

        
            System.Console.WriteLine("ReceivedMessage: {0}", message);
        }

    }
}
