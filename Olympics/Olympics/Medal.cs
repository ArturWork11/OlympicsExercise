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
        public Athlete? Athlete { get; set; }
        public Competition? Competition { get; set; }
        public Event? Event  { get; set; }
        public string MedalTier { get; set; }

        #endregion

        #region Methods
        public override string ToString()
        {
            return "\nMedal " + base.ToString() + $"Medal Tier: {MedalTier ?? "Unknown"} \nAthlete id: {Athlete.Id} \nAthlete: {Athlete?.AthleteName ?? "Unknown"} {Athlete?.Surname ?? "Unknown"} \nCompetition: {Competition?.CompetitionName ?? "Unknown"} \nEvent: {Event?.EventName ?? "Unknown"} \nMedal Tier: {MedalTier ?? "Unknown"} \nEvent Year: {Event?.EventYear}\n";
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

        public override void TypeSort(Dictionary<string, string> line)
        {
            base.TypeSort(line);

            if (line.TryGetValue("idathlete", out string athleteId) && int.TryParse(athleteId, out int athleteIdValue))
            {
                Athlete = DAOAthletes.GetInstance().FindRecord(athleteIdValue) as Athlete;
            }
            else
            {
                Athlete = null;
            }

            if (line.TryGetValue("idcompetition", out string competitionId) && int.TryParse(competitionId, out int competitionIdValue))
            {
                Competition = DAOCompetitions.GetInstance().FindRecord(competitionIdValue) as Competition;
            }
            else
            {
                Competition = null;
            }

            if (line.TryGetValue("idevent", out string eventId) && int.TryParse(eventId, out int eventIdValue))
            {
                Event = DAOEvents.GetInstance().FindRecord(eventIdValue) as Event;
            }
            else
            {
                Event = null;
            }
        }

        #endregion
    }
}
