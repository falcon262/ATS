using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Applications
{
    public enum ApplicationStatus
    {
        New = 0,
        InReview = 1,
        Shortlisted = 2,
        Interview = 3,
        Offered = 4,
        Hired = 5,
        Rejected = 6,
        Withdrawn = 7,
        OnHold = 8
    }

    public enum PipelineStage
    {
        Applied = 0,
        Screening = 1,
        PhoneScreen = 2,
        FirstInterview = 3,
        TechnicalAssessment = 4,
        FinalInterview = 5,
        ReferenceCheck = 6,
        Offer = 7,
        Hired = 8,
        Rejected = 9
    }
}
