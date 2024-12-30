using MSSTU.DB.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympics
{
    internal class Athlete : Entity
    {
        #region Properties
        public string AthleteName { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }

        public List<Medal> Medals { get; set; }
        #endregion

        #region Methods
        public override string ToString()
        {
            return base.ToString() + $"Name: {AthleteName} \nSurname: {Surname} \nDate of birth: {DateOfBirth.ToString("yyyy-MM-dd")} \nCountry: {Country}\n";
        }

        public  void FromDictionary(Dictionary<string, string> dictionary)
        {
            if (dictionary.ContainsKey("id"))
            {
                Id = int.Parse(dictionary["id"]);
            }
            if (dictionary.ContainsKey("athleteName"))
            {
                AthleteName = dictionary["athleteName"];
            }
            if (dictionary.ContainsKey("surname"))
            {
                Surname = dictionary["surname"];
            }
            if (dictionary.ContainsKey("dateOfBirth"))
            {
                DateOfBirth = DateTime.Parse(dictionary["dateOfBirth"]);
            }
            if (dictionary.ContainsKey("country"))
            {
                Country = dictionary["country"];
            }
        }
    }
    #endregion

}
