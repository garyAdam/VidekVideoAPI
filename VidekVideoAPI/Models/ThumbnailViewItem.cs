using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VidekVideoAPI.Models
{
    public class ThumbnailViewItem
    {
        [Key]
        public int Id { get; set; }
        public int VideoId { get; set; }
        public string Title { get; set; }
        public string ThumbnailURL{ get; set; }
        public string VideoURL { get; set; }

        [ForeignKey(nameof(VideoId))]
        public Video Video { get; set; }
    }
}
