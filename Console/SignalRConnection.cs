using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    public class SignalRConnection
    {
        private HubConnection? _connection;
        public HubConnection? HubConnection => _connection;
        public async void Start()
        {
            var url = "http://localhost:58561/streamHub";

             _connection = new HubConnectionBuilder()
                .WithUrl(url)
                .WithAutomaticReconnect()
                .Build();

            try
            {
                await _connection.StartAsync();
                _connection.On<string>("ReceiveMessage", (message) => OnReceiveMessage(message));
                System.Console.WriteLine("Connected.....");
            }
            catch (Exception ex)
            {
                OnDisconnected();
                Debug.WriteLine(ex.Message);    
            }


        }
        void OnDisconnected()
        {
            Thread.Sleep(5000);
            Start();
        }
        private void OnReceiveMessage( string message)
        {
            System.Console.WriteLine("ReceivedMessage: {0}", message);
        }

    }
}
