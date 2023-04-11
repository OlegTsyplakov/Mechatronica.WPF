using Accessibility;
using Mechatronica.DB.Interfaces;
using Mechatronica.WPF.Helpers;
using Mechatronica.WPF.Interfaces;
using Mechatronica.WPF.ViewModels;
using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
        private readonly IRepository _repository;

        private BaseModel(ConcurrentQueue<string> mockData, int interval, MainViewModel mainViewModel, IRepository repository)
        {
            _initialData = mockData;
            _interval = interval;
            _syncContext = SynchronizationContext.Current;
           _mainViewModel = mainViewModel;
            _repository = repository;
            OnUpdateList += AddItem;
            _mainViewModel.OnStartLoading += StartLoading;
            _mainViewModel.OnStopLoading += StopLoading;

        }
       public static BaseModel<T> Create(ConcurrentQueue<string> mockData, int interval, MainViewModel mainViewModel, IRepository repository)
        {
            return new BaseModel<T>(mockData, interval, mainViewModel, repository);    
        }

        void AddItem(T item)
        {
            _count = 0;
            
           
            _syncContext?.Post(o =>
            {
                MapToModel(item);
                _observableCollection.Add(item);
                _mainViewModel.AddToMatchDictionary(GetKeyValuePair(item));
            }, null);
        }

        KeyValuePair<string, IModel> GetKeyValuePair(T item)
        {
            return new KeyValuePair<string, IModel>(item.Date,item);
        }

        async void TimerElapsed(object sender, ElapsedEventArgs args)
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
                    _repository.AddCar(car);
                    return;
                case nameof(PersonModel):
                    var person = DbHelper.MapToPersonDbModel(item as PersonModel);
                    _repository.AddPerson(person);
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

        public ObservableCollection<T> Items
        {
            get { return _observableCollection; }

        }

        async Task Tik()
        {
            _count = 0;
            await Task.Run(() => {
                if (_initialData.TryDequeue(out string? name))
                {
                    var item = new T(){ Name = name, Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") };
                    OnUpdateList?.Invoke(item);

                }

            });
         

        }

    }


}
