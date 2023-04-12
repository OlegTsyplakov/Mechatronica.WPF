using System;
using System.Timers;
using Mechatronica.WPF.Interfaces;

namespace Mechatronica.WPF.Models
{
    public class PersonModel : IModel
    {
        public string Name { get; set; } = String.Empty;
        public string Date { get; set; } = String.Empty;

        public override string ToString()
        {
            return String.Format("Date: {0}, Person: {1}", Date, Name);
        }
    }
}
