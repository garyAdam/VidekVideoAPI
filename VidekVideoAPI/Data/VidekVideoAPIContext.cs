using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VidekVideoAPI.Models
{
    public class VidekVideoAPIContext : DbContext
    {
        public VidekVideoAPIContext (DbContextOptions<VidekVideoAPIContext> options)
            : base(options)
        {
        }

        public DbSet<VidekVideoAPI.Models.Video> Video { get; set; }
    }
}
