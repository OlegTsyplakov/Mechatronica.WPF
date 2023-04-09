using Mechatronica.WPF.Interfaces;
using System;
using System.Timers;

namespace Mechatronica.WPF.Models
{
    public class CarModel : IModel
    {
        public string Name { get; set; } = String.Empty;
        public string Date { get; set; } = String.Empty;

    }
}
