﻿using Microsoft.Extensions.Configuration;
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
using Mechatronica.DB.Interfaces;
using Mechatronica.DB.Repository;
using Mechatronica.WPF.SignalR;
using Mechatronica.WPF.Helpers;


namespace Mechatronica.WPF
{

    public partial class App : Application
    {

        private IHost AppHost { get; set; }
        private string? _signalRconnectionString;
        public App()
        {
      
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();


            var loggingSettings = configuration.GetSection("AppSettings:LogFilePath");
            _signalRconnectionString = configuration.GetSection("AppSettings:SignalRConnectionString").Value;
            var connectionString = configuration.GetSection("DataBaseSettings:ConnectionString");

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
                    services.Configure<DataBaseSettings>(connectionString);
                    services.AddSingleton<App>();
                    services.AddSingleton<IRepository, Repository>();
                    services.AddSingleton<ISignalRConnection>(s=>new SignalRConnection(_signalRconnectionString));
                    services.AddSingleton<MainWindow>();


                    services.AddDbContext<AppDbContext>(options =>
                                    options.UseSqlServer(connectionString.Value));

                })
                .Build();

        }
        protected override void OnStartup(StartupEventArgs e)
        {

            AppHost.RunAsync();
            Log.Logger.Information("Программа открыта");
            var appDbContext = AppHost.Services.GetRequiredService<AppDbContext>();

            DbHelper.CheckDataBaseConnectionAsync(AppHost, appDbContext);
            appDbContext.Database.EnsureCreated();
            appDbContext.Database.Migrate();
            IRepository repository = new Repository(appDbContext);
            ISignalRConnection signalRConnection = new SignalRConnection(_signalRconnectionString);
            var startForm = AppHost.Services.GetRequiredService<MainWindow>();
        
            startForm.DataContext = new MainViewModel(repository, signalRConnection);
     

            startForm.Show();
         
            base.OnStartup(e);
        }
        protected override void OnExit(ExitEventArgs e)
        {
            Log.Logger.Information("Программа закрыта");
            base.OnExit(e);
        }

    
    }
}
