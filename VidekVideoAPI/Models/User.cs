using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace VidekVideoAPI.Models
{
    public class User : IdentityUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public IList<Video> Videos { get; set; }

    }
}