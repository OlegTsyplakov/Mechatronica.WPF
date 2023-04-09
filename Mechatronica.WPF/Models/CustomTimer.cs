
using System.Timers;

namespace Mechatronica.WPF.Models
{
    public class CustomTimer
    {
       private static Timer _timer;


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
    }
}
