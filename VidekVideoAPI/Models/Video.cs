using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VidekVideoAPI.Models
{
    public class Video
    {
        private static int IdCounter = 0;
        public int Id { get; set; }
        public string Title { get; set; }
        public string Descirption { get; set; }
        public string SourcePath { get; set; }
        public string StreamURL { get; set; }

        public Video()
        {
            Id = IdCounter + 1;
            IdCounter++;
        }
    }
}
