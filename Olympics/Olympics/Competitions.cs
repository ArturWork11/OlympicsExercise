using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympics
{
    internal class Competitions : Medals
    {
        public string CompetitionName { get; set; }
        public string Category { get; set; }
        public bool IsIndoor { get; set; }
        public bool IsTeamCompetition { get; set; }

        public override string ToString()
        {
            return base.ToString() + $"Competition Name: {CompetitionName} \nCategory: {Category} \nIs the competition indoor: {IsIndoor} \nIs a team competition {IsTeamCompetition}\n";
        }
    }
}
