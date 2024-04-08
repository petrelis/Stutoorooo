using System.ComponentModel.DataAnnotations.Schema;

namespace Stutooroo.Models
{
    public class CommentImage
    {
        public int Id { get; set; }
        public Comment Comment { get; set; }
        [ForeignKey("Comment")]
        public int CommentId { get; set; }
        public string ImagePath { get; set; }
    }
}
