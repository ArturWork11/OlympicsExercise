using MSSTU.DB.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympics
{
   internal class Medal : Entity
    {
        #region Properties
        public Athlete Athlete { get; set; }
        public Competition Competition { get; set; }
        public Event Event  { get; set; }
        public string MedalTier { get; set; }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"Athlete: {Athlete.AthleteName} \nCompetition: {Competition.CompetitionName} \nEvent: {Event.EventName} \nMedal Tier: {MedalTier}\n";
        }

        public void FromDictionary(Dictionary<string, string> dictionary)
        {
            if (dictionary.ContainsKey("id"))
            {
                Id = int.Parse(dictionary["id"]);
            }
            if (dictionary.ContainsKey("idAthlete"))
            {
                Athlete = new Athlete();
                if (dictionary["idAthlete"] != "NULL")
                {
                    Athlete.Id = int.Parse(dictionary["idAthlete"]);
                }
                else
                {
                    Athlete = null;
                }
            }
            if (dictionary.ContainsKey("idCompetition"))
            {
                Competition = new Competition();
                if (dictionary["idCompetition"] != "NULL")
                {
                    Competition.Id = int.Parse(dictionary["idCompetition"]);
                }
                else
                {
                    Competition = null;
                }
            }
            if (dictionary.ContainsKey("idEvent"))
            {
                Event = new Event();
                if (dictionary["idEvent"] != "NULL")
                {
                    Event.Id = int.Parse(dictionary["idEvent"]);
                }
                else
                {
                    Event = null;
                }
            }
            if (dictionary.ContainsKey("medalTier"))
            {
                MedalTier = dictionary["medalTier"];
            }
        }
        #endregion
    }
}
