using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Jobs.Dtos
{
    public class PublishJobInput
    {
        public Guid JobId { get; set; }
        public DateTime? ScheduledDate { get; set; }
    }
}
