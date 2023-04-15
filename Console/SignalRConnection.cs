using Microsoft.AspNetCore.SignalR.Client;


namespace Console
{
    public class SignalRConnection
    {
        private readonly HubConnection? _connection;
        public HubConnection? HubConnection => _connection;

        private readonly string _url = "http://localhost:58561/streamHub";

        public SignalRConnection()
        {
            _connection = new HubConnectionBuilder()
              .WithUrl(_url)
              .WithAutomaticReconnect()
              .Build();
        }
        public async void Start()
        {           
            try
            {
                await _connection.StartAsync();
                _connection.On<string>("ReceiveMessage", (message) => OnReceiveMessage(message));
                System.Console.WriteLine("Слушаем ...");
            }
            catch (Exception ex)
            {
                OnConnectionException();
                System.Console.WriteLine(ex.Message);    
            }


        }
        void OnConnectionException()
        {
            Thread.Sleep(5000);
            Start();
        }
        static void OnReceiveMessage( string message)
        {
            System.Console.WriteLine("ReceivedMessage: {0}", message);
        }

    }
}
