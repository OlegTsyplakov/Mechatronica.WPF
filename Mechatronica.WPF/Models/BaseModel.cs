﻿using Mechatronica.WPF.Helpers;
using Mechatronica.WPF.Interfaces;
using Mechatronica.WPF.ViewModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;


namespace Mechatronica.WPF.Models
{
    public delegate void updateList<T>(T item);
    public class BaseModel<T>  where T : IModel, new()
    {
        private byte _count;
        private static readonly ObservableCollection<T> _observableCollection = new();
        private readonly SynchronizationContext? _syncContext;
        private event updateList<T>? OnUpdateList;
        private readonly ConcurrentQueue<string> _initialData;
        private readonly int _interval;
        private readonly MainViewModel _mainViewModel;
        public ObservableCollection<T> Items => _observableCollection;
        private BaseModel(ConcurrentQueue<string> mockData, int interval, MainViewModel mainViewModel)
        {
            _initialData = mockData;
            _interval = interval;
            _syncContext = SynchronizationContext.Current;
           _mainViewModel = mainViewModel;
            OnUpdateList += AddItem;
            _mainViewModel.OnStartLoading += StartLoading;
            _mainViewModel.OnStopLoading += StopLoading;

        }
       public static BaseModel<T> Create(ConcurrentQueue<string> mockData, int interval, MainViewModel mainViewModel)
        {
            return new BaseModel<T>(mockData, interval, mainViewModel);    
        }

        void AddItem(T item)
        {
            _count = 0;
            _syncContext?.Post( o =>
            {
                MapToModel(item);
               _mainViewModel.AddToMatchDictionary(BaseModel<T>.GetKeyValuePair(item));
                _observableCollection.Add(item);
              
            }, null);
        }

        static KeyValuePair<string, IModel> GetKeyValuePair(T item)
        {
            return new KeyValuePair<string, IModel>(item.Date,item);
        }

        async void TimerElapsed(object? sender, ElapsedEventArgs args)
        {
            if (_count % _interval == 0)
            {
                await Tik();
            }
            _count++;
        }

        void MapToModel(T item)
        {
            var type = item.GetType().Name;
            switch (type)
            {
                case nameof(CarModel):
                    var car = DbHelper.MapToCarDbModel(item as CarModel);
                    _mainViewModel.Repository.AddCar(car);
                    return;
                case nameof(PersonModel):
                    var person = DbHelper.MapToPersonDbModel(item as PersonModel);
                    _mainViewModel.Repository.AddPerson(person);
                    return;
                default:
                    break;
            }
        }

        void StartLoading()
        {
            CustomTimer.Subscribe(TimerElapsed);
        }
        void StopLoading()
        {
            CustomTimer.UnSubscribe(TimerElapsed);
        }

        async Task Tik()
        {
            _count = 0;
            await Task.Run(() => {
                if (_initialData.TryDequeue(out string? name))
                {
                    var item = new T(){ Name = name, Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") };
                    _mainViewModel.Connection.Send(item.ToString());
                    OnUpdateList?.Invoke(item);

                }

            });
        }
     

    }


}
