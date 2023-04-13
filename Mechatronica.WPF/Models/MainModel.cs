using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechatronica.WPF.Models
{
    public class MainModel
    {
        public string? Person { get; set; }
        public string? Car { get; set; }
        public string? Date { get; set; }

        public override string ToString()
        {
            return String.Format("Date: {0}, Person: {1}, Car: {2}",Date,Person,Car);
        }
    }
}
