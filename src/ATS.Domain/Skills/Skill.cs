using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace ATS.Skills
{
    /// <summary>
    /// Master list of skills for categorization and searching
    /// </summary>
    public class Skill : Entity<Guid>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Category { get; set; } // Programming, Soft Skills, Tools, etc.

        [MaxLength(500)]
        public string? Description { get; set; }

        public bool IsActive { get; set; }

        // For synonyms and related terms (JSON array)
        public string? SynonymsJson { get; set; }

        public int UsageCount { get; set; } // Track popularity

        protected Skill()
        {
        }

        public Skill(Guid id, string name, string category = null) : base(id)
        {
            Name = name;
            Category = category;
            IsActive = true;
            UsageCount = 0;
        }

        public void IncrementUsage()
        {
            UsageCount++;
        }
    }
}
