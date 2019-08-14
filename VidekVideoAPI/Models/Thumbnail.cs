using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VidekVideoAPI.Models
{
    public class Thumbnail
    {
        [Key]
        public int VideoID { get; set; }
        public string SourcePath { get; set; }
    }
}
