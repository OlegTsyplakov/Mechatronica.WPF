using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Слушаем ...");
            var signalRConnection = new SignalRConnection();
        
                signalRConnection.Start();
          
          

            System.Console.Read();
        }
    }
}