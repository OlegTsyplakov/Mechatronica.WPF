using Mechatronica.WPF.Commands;
using Mechatronica.WPF.Models;
using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Timers;
using System.Windows.Input;

namespace Mechatronica.WPF.ViewModels
{


    public class MainViewModel : IDisposable
    {
        private readonly BaseModel<CarModel> _car;
        public ObservableCollection<CarModel> Cars => _car.Items ?? new ObservableCollection<CarModel>();

        private readonly BaseModel<PersonModel> _person;
        public ObservableCollection<PersonModel> Persons => _person.Items ?? new ObservableCollection<PersonModel>();

        private ConcurrentDictionary<string,string> _matchDictionary = new ConcurrentDictionary<string,string>();
        private ObservableCollection<MainModel> _mainModels;
        private bool disposedValue;

        public ObservableCollection<MainModel> MainModels => _mainModels;  
        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }

        public event Action? OnStartLoading;
        public event Action? OnStopLoading;

        public MainViewModel()
        {
            StartCommand = new RelayCommand((obj) =>
            {
               InvokeNotify(OnStartLoading,obj);
            }, CanExecute);
            StopCommand = new RelayCommand((obj) =>
            {
               InvokeNotify(OnStopLoading, obj);
            }, CanExecute);
            _mainModels = new ObservableCollection<MainModel>();
            _car = BaseModel<CarModel>.Create(MockData.Cars, 2, this);
            _person = BaseModel<PersonModel>.Create(MockData.Persons, 3, this);
            CustomTimer.Start();
            InvokeNotify(OnStartLoading);
        }
        bool CanExecute(object parameter)
        {
            return true;
        }
        public void AddToMatchDictionary(KeyValuePair<string, string> item)
        {
            if (_matchDictionary.TryAdd(item.Key,item.Value))
            {
                _matchDictionary.AddOrUpdate(item.Key, item.Value, (key, oldValue) => item.Value);
            }
            else
            {
                MainModel mainModel = new MainModel()
                {
                    Date = item.Key,
                    Person = item.Value,
                    Car = _matchDictionary[item.Key]
                };
                _mainModels.Add(mainModel);
            }
        }

        static void InvokeNotify(Action? action, object? obj = null)
        {
            if (obj == null)
            {
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
