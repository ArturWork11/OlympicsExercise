using MSSTU.DB.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympics
{
    internal class Event : Entity
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

        public void FromDictionary(Dictionary<string, string> dictionary)
        {
            if (dictionary.ContainsKey("id"))
            {
                Id = int.Parse(dictionary["id"]);
            }
            if (dictionary.ContainsKey("eventName"))
            {
                EventName = dictionary["eventName"];
            }
            if (dictionary.ContainsKey("eventYear"))
            {
                EventYear = int.Parse(dictionary["eventYear"]);
            }
            if (dictionary.ContainsKey("eventLocation"))
            {
                EventLocation = dictionary["eventLocation"];
            }
        }
        #endregion
    }
}
