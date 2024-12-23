using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympics
{
    internal class Athlete : Medal
    {
        #region Properties
        public string AthleteName { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        #endregion

        #region Methods
        public override string ToString()
        {
            return base.ToString() + $"Name: {AthleteName} \nSurname: {Surname} \nDate of birth: {DateOfBirth} \nCountry: {Country}\n";
        }
        #endregion
    }
}
