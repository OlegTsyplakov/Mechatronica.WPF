
using System;
using System.Threading.Tasks;
using System.Timers;

namespace Mechatronica.WPF.Models
{
    public class CustomTimer : IDisposable
    {
       private static readonly Timer _timer;
        private bool disposedValue;

        static CustomTimer()
        {
            _timer = new()
            {
                Interval = 1000
            };
        }
        public static void Start()
        {
            _timer.Start();
        }
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
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _timer.Dispose();
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
