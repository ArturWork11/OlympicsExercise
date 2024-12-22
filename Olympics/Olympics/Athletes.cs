using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympics
{
    internal class Athletes : Medals
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }

        public override string ToString()
        {
            return base.ToString() + $"Name: {Name} \nSurname: {Surname} \nDate of birth: {DateOfBirth} \nCountry: {Country}\n";
        }
    }
}
