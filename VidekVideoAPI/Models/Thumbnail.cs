using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VidekVideoAPI.Models
{
    public class Thumbnail
    {
        public Thumbnail()
        {
        }

        public Thumbnail(int id, string thumbnailFullPath)
        {
            VideoID = id;
            SourcePath = thumbnailFullPath;
        }
        [Key]
        public int VideoID { get; set; }
        public string SourcePath { get; set; }
    }
}
