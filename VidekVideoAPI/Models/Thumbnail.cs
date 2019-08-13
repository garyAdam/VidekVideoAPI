using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VidekVideoAPI.Models
{
    public class Thumbnail
    {

        public Thumbnail(int id, string thumbnailFullPath)
        {
            VideoID = id;
            SourcePath = thumbnailFullPath;
        }

        public int VideoID { get; set; }
        public string SourcePath { get; set; }
    }
}
