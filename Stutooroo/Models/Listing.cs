using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stutooroo.Models
{
    public class Listing
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DisplayName("Posted At Time")]
        public DateTime PostedAtDateTime { get; set; }
        [DisplayName("Experience Level")]
        public ExperienceLvl? ExperienceLvl { get; set; }
        [ForeignKey("ExperienceLvl")]
        [DisplayName("Experience Level")]
        public int ExperienceLvlId { get; set; }
        [DisplayName("Hourly Rate")]
        public float HourlyRate { get; set; }
        public float Rating { get; set; } = 10f;
        [DisplayName("Subject Group")]
        public SubjectGroup? SubjectGroup { get; set; }
        [ForeignKey("SubjectGroup")]
        [DisplayName("Subject Group")]
        public int SubjectGroupId { get; set;}
        [DisplayName("Subject")]
        public string Subject { get; set; }
        [ForeignKey("PostedByUser")]
        [DisplayName("Posted By User")]
        public string? PostedByUserId { get; set; }
        [DisplayName("Posted By User")]
        public virtual ApplicationUser? PostedByUser { get; set; }
    }
}
