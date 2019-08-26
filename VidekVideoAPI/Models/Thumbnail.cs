using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VidekVideoAPI.Models
{
    public class Thumbnail
    {

        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Video))]
        public int VideoID { get; set; }
        public Video Video { get; set; }
        public string SourcePath { get; set; }
    }
}
