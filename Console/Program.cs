

namespace Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Подключаемся ...");
            var signalRConnection = new SignalRConnection();
        
                signalRConnection.Start();
          
          

            System.Console.Read();
        }
    }
}