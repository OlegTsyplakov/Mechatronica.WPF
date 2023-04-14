using Mechatronica.WPF.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using Serilog;
using System;

using System.Threading.Tasks;


namespace Mechatronica.WPF.SignalR
{
    public class SignalRConnection : ISignalRConnection
    {
   
        readonly HubConnection _connection;



        public SignalRConnection(string url)
        {
       
            _connection = new HubConnectionBuilder()
               .WithUrl(url)
               .WithAutomaticReconnect()
               .Build();
        }


        private void OnReceiveMessage(string message)
        {
            Console.WriteLine("ReceivedMessage: {0}", message);
        }

        public async Task Start()
        {
            try
            {
                await _connection.StartAsync();
            }
            catch (Exception ex)
            {
                Log.Logger.Warning($"не удалось подключиться к серверу SignalR. {ex.Message}");
            }

         
        }

        public async Task Send(string message)
        {
            try
            {
                await _connection.InvokeAsync("SendMessageAsync", message);
            }
            catch (Exception ex)
            {
                Log.Logger.Warning($"не удалось отправить сообщение на сервер SignalR. {ex.Message}");
            }
         

        }

        public void Receive()
        {
            try
            {
                _connection.On<string>("ReceiveMessage", (message) => OnReceiveMessage(message));
            }
            catch (Exception ex)
            {
                Log.Logger.Warning($"не удалось получить сообщение с сервера SignalR. {ex.Message}");
            }
 
        }

        public async Task Stop()
        {
            try
            {
                await _connection.StopAsync();
            }
            catch (Exception ex)
            {
                Log.Logger.Warning($"не удалось оставноить сервер SignalR. {ex.Message}");
            }
     
        }

        public string Dummy(string dummy)
        {
            return dummy;
        }

    }
}