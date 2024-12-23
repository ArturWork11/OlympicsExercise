using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympics
{
    internal class Event : Medal
    {
        #region Properties
        public string EventName { get; set; }
        public int EventYear { get; set; }
        public string EventLocation { get; set; }
        #endregion

        #region Methods
        public override string ToString()
        {
            return base.ToString() + $"Event: {EventName}  \nYear: {EventYear} \nLocation: {EventLocation}\n";
        }
        #endregion
    }
}
