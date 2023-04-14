using Mechatronica.WPF.Interfaces;
using Mechatronica.WPF.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Mechatronica.Tests
{
    public class Setup : Xunit.Di.Setup
    {
        private IServiceProvider _services;
        private bool _built = false;
        private readonly IHostBuilder _defaultBuilder;

        public Setup()
        {
            _defaultBuilder = Host.CreateDefaultBuilder();
        }

        public IServiceProvider Services => _services ?? Build();

        private IServiceProvider Build()
        {
            if (_built)
                throw new InvalidOperationException("Build can only be called once.");
            _built = true;

            _defaultBuilder.ConfigureServices((context, services) =>
            {

                services.AddSingleton<ISignalRConnection>(s => new SignalRConnection("http://localhost:58561/streamHub"));

            });

            _services = _defaultBuilder.Build().Services;
            return _services;
        }
    }
}
