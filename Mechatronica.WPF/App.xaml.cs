using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog.Events;
using Serilog;

using System.IO;

using System.Windows;

using Microsoft.Extensions.DependencyInjection;
using Mechatronica.WPF.Settings;
using Mechatronica.DB;
using Microsoft.EntityFrameworkCore;
using System;
using Mechatronica.WPF.Views;
using Mechatronica.WPF.Interfaces;
using Mechatronica.WPF.ViewModels;



namespace Mechatronica.WPF
{

    public partial class App : Application
    {
   
        public IHost AppHost { get; private set; }
        public App()
        {
      
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();


            var loggingSettings = configuration.GetSection("AppSettings:LogFilePath");
            var connectionStriing = configuration.GetSection("DataBaseSettings:ConnectionString");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .WriteTo.File(loggingSettings.Value, rollingInterval: RollingInterval.Hour, retainedFileCountLimit: null)
                .CreateLogger();

            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.Configure<AppSettings>(loggingSettings);
                    services.Configure<DataBaseSettings>(connectionStriing);
                    services.AddSingleton<App>();

              
                    services.AddSingleton<MainWindow>();
                    services.AddDbContext<AppDbContext>(options =>
                            options.UseSqlServer(connectionStriing.Value));

                })
                .Build();

        }
        protected override void OnStartup(StartupEventArgs e)
        {
            AppHost.Start();
    
           
            var startForm = AppHost.Services.GetRequiredService<MainWindow>();
        
            startForm.DataContext = new MainViewModel();
     

            startForm.Show();
            Log.Logger.Information("Программа открыта");
            base.OnStartup(e);
        }
        protected override void OnExit(ExitEventArgs e)
        {
            Log.Logger.Information("Программа закрыта");
            base.OnExit(e);
        }
    
    }
}
