using Mechatronica.WPF.Interfaces;
using System;
using System.Runtime.ConstrainedExecution;
using System.Timers;

namespace Mechatronica.WPF.Models
{
    public class CarModel : IModel
    {
        public string? Name { get; set; }
        public string? Date { get; set; }

        public override string ToString()
        {
            return String.Format("Date: {0}, Car: {1}", Date, Name);
        }

    }

}
