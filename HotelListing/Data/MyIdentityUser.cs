using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.Data
{
    public class MyIdentityUser : IdentityUser
    {
        public MyIdentityUser() { }

        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}
