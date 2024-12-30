using MSSTU.DB.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympics
{
    internal class Competition : Entity
    {
        #region Properties
        public string CompetitionName { get; set; }
        public string Category { get; set; }
        public bool IsIndoor { get; set; }
        public bool IsTeamCompetition { get; set; }
        #endregion

        #region Methods
        public override string ToString()
        {
            return base.ToString() + $"Competition Name: {CompetitionName} \nCategory: {Category} \nIs the competition indoor: {(IsIndoor ? "yes" : "no")} \nIs a team competition {(IsTeamCompetition ? "yes" : "no")}\n";
        }

        public void FromDictionary(Dictionary<string, string> dictionary)
        {
            if (dictionary.ContainsKey("id"))
            {
                Id = int.Parse(dictionary["id"]);
            }
            if (dictionary.ContainsKey("competitionName"))
            {
                CompetitionName = dictionary["competitionName"];
            }
            if (dictionary.ContainsKey("category"))
            {
                Category = dictionary["category"];
            }
            if (dictionary.ContainsKey("isIndoor"))
            {
                IsIndoor = dictionary["isIndoor"] == "1";
            }
            if (dictionary.ContainsKey("isTeamCompetition"))
            {
                IsTeamCompetition = dictionary["isTeamCompetition"] == "1";
            }
        }
        #endregion
    }
}
