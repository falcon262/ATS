using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Jobs
{
    public enum JobStatus
    {
        Draft = 0,
        Active = 1,
        Paused = 2,
        Closed = 3,
        Archived = 4
    }

    public enum EmploymentType
    {
        FullTime = 0,
        PartTime = 1,
        Contract = 2,
        Temporary = 3,
        Internship = 4,
        Freelance = 5
    }

    public enum ExperienceLevel
    {
        EntryLevel = 0,
        Junior = 1,
        MidLevel = 2,
        Senior = 3,
        Lead = 4,
        Executive = 5
    }
}
