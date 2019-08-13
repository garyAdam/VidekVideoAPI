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
        public string ThumbnailPath { get; set; }

        public Video()
        {
        }

        public Video(int id, string title, string descirption, string sourcePath, string thumbnailPath)
        {
            Id = id;
            Title = title;
            Descirption = descirption;
            SourcePath = sourcePath;
            ThumbnailPath = thumbnailPath;
        }
    }
}
