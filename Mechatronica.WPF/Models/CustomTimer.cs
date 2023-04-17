
using System;
using System.Threading.Tasks;
using System.Timers;

namespace Mechatronica.WPF.Models
{
    public static class CustomTimer 
    {
       private static readonly Timer _timer;
        private static int _ticks;
        public static int Ticks => _ticks;

        private static readonly ElapsedEventHandler OnTick;
        static CustomTimer()
        {
            _timer = new()
            {
                Interval = 1000
            };
            OnTick = Tick;
        }
        public static void Start()
        {
            _timer.Start();
            _timer.Elapsed += OnTick;
        }
        public static void Stop()
        {
            _timer.Elapsed -= OnTick;
            _timer.Stop();
        }
        private static void Tick(object? sender, ElapsedEventArgs args) => _ticks++;
        public static void Subscribe(ElapsedEventHandler Elapsed)
        {
            _timer.Elapsed += Elapsed; 
        }
        public static void UnSubscribe(ElapsedEventHandler Elapsed)
        {
            _timer.Elapsed -= Elapsed;
        }
        public static async Task Tik(Action action)
        {
                await Task.Run(() => {
                    action.Invoke();
                });
        }

    }
}
