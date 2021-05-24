using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsIdentity.Models
{
    public class User:IdentityUser
    {
        public string Address { get; set; }

        public static implicit operator IdentityResult(User v)
        {
            throw new NotImplementedException();
        }
    }
}