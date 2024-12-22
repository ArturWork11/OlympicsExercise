using MSSTU.DB.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympics
{
    internal class Medals : Entity
    {
        public int IdAthlete { get; set; }
        public int IdCompetition { get; set; }
        public int IdEvent { get; set; }
        public string MedalTier { get; set; }

        public override string ToString()
        {
            return $"IdAthlete: {IdAthlete} \nIdCompetition: {IdCompetition} \nIdEvent: {IdEvent} \nMedal Tier: {MedalTier}\n";
        }
    }
}
