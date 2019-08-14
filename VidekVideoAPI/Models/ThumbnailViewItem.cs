using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VidekVideoAPI.Models
{
    public class ThumbnailViewItem
    {
        [Key]
        public int VideoId { get; set; }
        public string Title { get; set; }
        public string ThumbnailURL{ get; set; }
        public string VideoURL { get; set; }
    }
}
