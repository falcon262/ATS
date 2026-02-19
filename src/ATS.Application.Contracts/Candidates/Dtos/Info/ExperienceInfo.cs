using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Candidates.Dtos.Info
{
    public class ExperienceInfo
    {
        public string JobTitle { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrent { get; set; }
        public string Description { get; set; }
        public List<string> KeyAchievements { get; set; }
    }
}
