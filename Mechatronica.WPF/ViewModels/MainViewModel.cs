using Mechatronica.DB.Interfaces;
using Mechatronica.DB.Models;
using Mechatronica.WPF.Commands;
using Mechatronica.WPF.Helpers;
using Mechatronica.WPF.Interfaces;
using Mechatronica.WPF.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;


namespace Mechatronica.WPF.ViewModels
{


    public class MainViewModel : IDisposable
    {
   
        private readonly BaseModel<CarModel> _car;
        public ObservableCollection<CarModel> Cars => _car.Items;

        private readonly BaseModel<PersonModel> _person;
        private readonly IRepository _repository;
        private readonly ISignalRConnection _connection;
        public ObservableCollection<MainDbModel> DbData => _repository.GetObservableCollectionMainDbModel();
        public ObservableCollection<PersonModel> Persons => _person.Items;

        private readonly ConcurrentDictionary<string, IModel> _matchDictionary = new();

        private readonly ObservableCollection<MainModel> _mainModels = new();
        private bool disposedValue;

        public ObservableCollection<MainModel> MainModels =>_mainModels;
    
        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }

        public ISignalRConnection Connection => _connection;

        public event Action? OnStartLoading;
        public event Action? OnStopLoading;
   

        private readonly SynchronizationContext? _syncContext;

        public MainViewModel(IRepository repository, ISignalRConnection connection)
        {
            _repository = repository;
            _connection = connection;
           StartCommand = new RelayCommand((obj) =>
            {
               InvokeNotify(OnStartLoading,obj);
            }, CanExecute);
            StopCommand = new RelayCommand((obj) =>
            {
               InvokeNotify(OnStopLoading, obj);
            }, CanExecute);
            _car = BaseModel<CarModel>.Create(MockData.Cars, 2, this, _repository);
            _person = BaseModel<PersonModel>.Create(MockData.Persons, 3, this, _repository);
       
            CustomTimer.Start();
            InvokeNotify(OnStartLoading);
            CustomTimer.Subscribe(TimerElapsed);
            _syncContext = SynchronizationContext.Current;
             _connection.Start();
        }
      
        bool CanExecute(object parameter)
        {
            return true;
        }
        public void AddToMatchDictionary(KeyValuePair<string, IModel> item)
        {

            if (_matchDictionary.TryAdd(item.Key,item.Value))
            {
                _matchDictionary.AddOrUpdate(item.Key, item.Value, (key, oldValue) => item.Value);
                MainModel mainModel = DbHelper.MapToMainModel(item,null);
                var mainDbModel = DbHelper.MapToMainDbModel(mainModel);
              _repository.AddMain(mainDbModel);
                _connection.Send(mainModel.ToString());
            }
            else
            {
                MainModel mainModel = DbHelper.MapToMainModel(item, _matchDictionary[item.Key].Name);

                _mainModels.Add(mainModel);
                var mainDbModel = DbHelper.MapToMainDbModel(mainModel);
                 _repository.UpdateMain(mainDbModel);
                _matchDictionary.Clear();
            }
        }
    
        void TimerElapsed(object? sender, ElapsedEventArgs args)
        {
            _syncContext?.Post( o =>
            {
                var DbDataExt = _repository.GetAll().AsEnumerable();
                var difference = DbDataExt.Count() - DbData.Count;
                if (difference > 0)
                {
                    var items = DbDataExt.TakeLast(difference);
                    foreach (var item in items)
                    {
                        DbData.Add(item);
                    }
                }
 
            }, null);
        }
  
        static void InvokeNotify(Action? action, object? obj = null)
        {
            if (obj == null)
            {
                Log.Logger.Information("Запущен общий поток выполнения.");
                action?.Invoke();
            }
            else
            {
                var multiDelegate = action?.GetInvocationList();

                if (multiDelegate != null)
                {

                    foreach (Delegate d in multiDelegate)
                    {
                        var target = d.Target?.GetType().GetGenericArguments()[0];

                        if (target != null && target.Name == obj.ToString())
                        {
                            Log.Logger.Information(String.Format("Вызвано действие {0} на {1}.",d.Method.Name,target.Name));
                            d.DynamicInvoke();
                        }

                    }

                }
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    OnStartLoading = null;
                    OnStopLoading = null;
                    _connection.Stop();
                }

                disposedValue = true;
            }
        }


        public void Dispose()
        {

            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }



}
