using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace VidekVideoAPI.Models
{
    public class User : IdentityUser
    {
        public IList<Video> Videos { get; set; }

    }
}