using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Applications.Dtos
{
    public class MakeOfferInput
    {
        public Guid ApplicationId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal OfferedSalary { get; set; }

        public DateTime OfferExpiryDate { get; set; }

        public string OfferDetails { get; set; }
    }
}
