using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stutooroo.Models
{
    public class Listing
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime PostedAtDateTime { get; set; }
        public ExperienceLvl? ExperienceLvl { get; set; }
        [ForeignKey("ExperienceLvl")]
        public int ExperienceLvlId { get; set; }
        public float HourlyRate { get; set; }
        public float Rating { get; set; } = 10f;
        public SubjectGroup? SubjectGroup { get; set; }
        [ForeignKey("SubjectGroup")]
        public int SubjectGroupId { get; set;}
        public string Subject { get; set; }
        [ForeignKey("PostedByUser")]
        public string? PostedByUserId { get; set; }
        public virtual IdentityUser? PostedByUser { get; set; }
    }
}
