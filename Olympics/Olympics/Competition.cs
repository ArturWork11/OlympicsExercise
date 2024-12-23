using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympics
{
    internal class Competition : Medal
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
        #endregion
    }
}
