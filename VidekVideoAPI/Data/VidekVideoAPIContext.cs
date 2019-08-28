using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VidekVideoAPI.Models;

namespace VidekVideoAPI.Models
{
    public class VidekVideoAPIContext : DbContext
    {
        public VidekVideoAPIContext (DbContextOptions<VidekVideoAPIContext> options)
            : base(options)
        {
        }

        public DbSet<VidekVideoAPI.Models.Video> Videos { get; set; }
        public DbSet<VidekVideoAPI.Models.Thumbnail> Thumbnails { get; set; }
        public DbSet<VidekVideoAPI.Models.ThumbnailViewItem> ThumbnailViewItems { get; set; }


    }
}
