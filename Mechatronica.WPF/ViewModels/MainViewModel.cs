using Mechatronica.DB.Interfaces;
using Mechatronica.DB.Models;
using Mechatronica.WPF.Commands;
using Mechatronica.WPF.Helpers;
using Mechatronica.WPF.Interfaces;
using Mechatronica.WPF.Models;
using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

namespace Mechatronica.WPF.ViewModels
{


    public class MainViewModel : IDisposable
    {

        private readonly BaseModel<CarModel> _car;
        public ObservableCollection<CarModel> Cars => _car.Items ?? new ObservableCollection<CarModel>();

        private readonly BaseModel<PersonModel> _person;
        private readonly IRepository _repository;

        public ObservableCollection<MainModel> _dbData = new ObservableCollection<MainModel>();
        public ObservableCollection<MainModel> DbData => _dbData;
        public ObservableCollection<PersonModel> Persons => _person.Items ?? new ObservableCollection<PersonModel>();

        private ConcurrentDictionary<string, IModel> _matchDictionary = new ConcurrentDictionary<string, IModel>();

        private readonly ObservableCollection<MainModel> _mainModels = new ObservableCollection<MainModel>();
        private bool disposedValue;

        public ObservableCollection<MainModel> MainModels =>_mainModels;
    
        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }

        public event Action? OnStartLoading;
        public event Action? OnStopLoading;



        public MainViewModel(IRepository repository)
        {
            _repository = repository;
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
                MainModel mainModel = MapToMainModel(item);
                var mainDbModel = DbHelper.MapToMainDbModel(mainModel);
                _repository.AddMain(mainDbModel);

            }
            else
            {
                MainModel mainModel = MapToMainModel(item, _matchDictionary[item.Key].Name);

                _mainModels.Add(mainModel);
                var mainDbModel = DbHelper.MapToMainDbModel(mainModel);
                _repository.UpdateMain(mainDbModel);
                _dbData.Add(mainModel);
                _matchDictionary.Clear();
            }
        }

        private MainModel MapToMainModel(KeyValuePair<string, IModel> item, string previousName = "")
        {
            MainModel mainModel = new MainModel()
            {
                Date = item.Key,
            };
            if (item.Value.GetType().Name != nameof(CarModel))
            {
                mainModel.Person = item.Value.Name;
                mainModel.Car = previousName;
            }
            else
            {
                mainModel.Car = item.Value.Name;
                mainModel.Person = previousName;
            }
            return mainModel;
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
