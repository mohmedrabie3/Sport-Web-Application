using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SportsIdentity.Models
{
    public class Modelcontext:IdentityDbContext<User>
    {
        public Modelcontext():base("con")
        {

        }
        public Modelcontext( string connection):base(connection)
        {

        }
        public DbSet<Player> players { get; set; }
        public DbSet<FootBallTeam> footBallTeams { get; set; }
    }
}