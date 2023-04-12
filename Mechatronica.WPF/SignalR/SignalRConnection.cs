using Mechatronica.WPF.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using System;

using System.Threading.Tasks;


namespace Mechatronica.WPF.SignalR
{
    public class SignalRConnection : ISignalRConnection
    {
        private readonly string? _url;
        readonly HubConnection _connection;



        public SignalRConnection(string? url)
        {
            _url = url;
            _connection = new HubConnectionBuilder()
               .WithUrl(_url)
               .WithAutomaticReconnect()
               .Build();
        }


        private void OnReceiveMessage(string message)
        {
            Console.WriteLine("ReceivedMessage: {0}", message);
        }

        public async Task Start()
        {
          await _connection.StartAsync();
         
        }

        public async Task Send(string message)
        {
            await _connection.InvokeAsync("SendMessageAsync", message);

        }

        public void Receive()
        {
            _connection.On<string>("ReceiveMessage", (message) => OnReceiveMessage(message));
        }

        public async Task Stop()
        {
            await _connection.StopAsync();
        }



    }
}