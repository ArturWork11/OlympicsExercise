using MSSTU.DB.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympics
{
        #region Properties
    internal class Medal : Entity
    {
        public Athlete Athlete { get; set; }
        public Competition Competition { get; set; }
        public Event Event  { get; set; }
        public string MedalTier { get; set; }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"Athlete: {Athlete.Name} \nCompetition: {Competition.CompetitionName} \nEvent: {Event.EventName} \nMedal Tier: {MedalTier}\n";
        }
        #endregion
    }
}
