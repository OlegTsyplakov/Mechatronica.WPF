using Mechatronica.DB;
using Mechatronica.DB.Interfaces;
using Mechatronica.DB.Repository;
using Mechatronica.WPF;
using Mechatronica.WPF.Interfaces;
using Mechatronica.WPF.Settings;
using Mechatronica.WPF.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Mechatronica.Tests
{
    public class Setup : Xunit.Di.Setup
    {
        private IServiceProvider _services;
        private bool _built = false;
        private readonly IHostBuilder _defaultBuilder;
        private string? _signalRconnectionString;
        private string? _dbconnectionString;

        public Setup()
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.test.json", optional: false, reloadOnChange: true)
            .Build();
            _signalRconnectionString = configuration.GetSection("AppSettings:SignalRConnectionString").Value;
            _dbconnectionString = configuration.GetSection("DataBaseSettings:ConnectionString").Value;
            _defaultBuilder = Host.CreateDefaultBuilder();
    
        }
        public IHost AppHost =>_defaultBuilder.Build();
        public new IServiceProvider Services => _services ?? Build();

        private IServiceProvider Build()
        {
            if (_built)
                throw new InvalidOperationException("Build can only be called once.");
            _built = true;

            _defaultBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<IRepository, Repository>();
                services.AddSingleton<ISignalRConnection>(s => new SignalRConnection(_signalRconnectionString));
                services.AddDbContext<AppDbContext>(options =>
                            options.UseSqlServer(_dbconnectionString));

            });
          
            _services = _defaultBuilder.Build().Services;
            return _services;
        }
    }
}
