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


    public class MainViewModel
    {
        private readonly BaseModel<CarModel> _car;
        public ObservableCollection<CarModel> Cars => _car.Items ?? new ObservableCollection<CarModel>();

        private readonly BaseModel<PersonModel> _person;
        public ObservableCollection<PersonModel> Persons => _person.Items ?? new ObservableCollection<PersonModel>();

        private ConcurrentDictionary<string,string> _matchDictionary = new ConcurrentDictionary<string,string>();
        private ObservableCollection<MainModel> _mainModels;

        public ObservableCollection<MainModel> MainModels => _mainModels;  
        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }

        public event Action? OnStartLoading;
        public event Action? OnStopLoading;

        public MainViewModel()
        {
            StartCommand = new RelayCommand((obj) =>
            {
               StartNotify(obj);
            }, CanExecute);
            StopCommand = new RelayCommand((obj) =>
            {
               StopNotify(obj);
            }, CanExecute);
            _mainModels = new ObservableCollection<MainModel>();
            _car = BaseModel<CarModel>.Create(MockData.Cars, 2, this);
            _person = BaseModel<PersonModel>.Create(MockData.Persons, 3, this);
            CustomTimer.Start();
            StartNotify();
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
        void StartNotify(object? obj = null)
        {
            if(obj == null)
            {
                OnStartLoading?.Invoke();
            }
            else
            {
                var multiDelegate = OnStartLoading?.GetInvocationList();

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

        void StopNotify(object obj)
        {
            if (obj == null)
            {
                OnStopLoading?.Invoke();
            }
            else
            {
                var multiDelegate = OnStopLoading?.GetInvocationList();
             
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
    }



}
