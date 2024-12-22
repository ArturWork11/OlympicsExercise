using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympics
{
    internal class Events : Medals
    {
        public string EventName { get; set; }
        public int Year { get; set; }
        public string Location { get; set; }
        public override string ToString()
        {
            return base.ToString() + $"Event: {EventName}  \nYear: {Year} \nLocation: {Location}\n";
        }
    }
}
