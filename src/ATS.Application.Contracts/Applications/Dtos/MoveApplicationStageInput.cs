using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Applications.Dtos
{
    public class MoveApplicationStageInput
    {
        public Guid ApplicationId { get; set; }
        public PipelineStage NewStage { get; set; }
        public string Notes { get; set; }
    }
}
