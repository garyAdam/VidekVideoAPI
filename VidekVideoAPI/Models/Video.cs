using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VidekVideoAPI.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Descirption { get; set; }
        public string SourcePath { get; set; }
        public User Uploader { get; set; }
        public Thumbnail Thumbnail { get; set; }
    }
}
