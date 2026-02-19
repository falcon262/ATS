using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Applications.Dtos
{
    public class SkillMatchDto
    {
        public string Skill { get; set; } = string.Empty;
        public bool HasIt { get; set; }
        public int YearsExperience { get; set; }
        public string Weight { get; set; } = string.Empty; // "Required" or "Preferred"
    }
}

