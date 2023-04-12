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


        //private void OnReceiveMessage(string message)
        //{
        //   Console.WriteLine("ReceivedMessage: {0}", message);
        //}

        // public ISignalRConnectionAction MyStart()
        //{
        //    //await _connection.StartAsync();
        //    return this;
        //}

        //public async void MySend()
        //{
        //    await _connection.InvokeAsync("SendMessage", "ConsoleApp", "Message from the console app");

        //}

        //public void MyReceive(string message)
        //{
        //    _connection.On<string>("ReceiveMessage", (message) => OnReceiveMessage(message));
        //}

        //public async void MyStop()
        //{
        //    await _connection.StopAsync();
        //}



    }
}