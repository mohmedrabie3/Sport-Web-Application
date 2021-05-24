using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportsIdentity.Models
{
    public class FootBallTeam
    {
        public FootBallTeam()
        {
            Players = new HashSet<Player>();
        }

        public int ID { get; set; }

        [Required(ErrorMessage ="*")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage ="*")]
        [StringLength(50)]
        public string Country { get; set; }

        public int? FoundationDate { get; set; }
        [Required(ErrorMessage = "*")]
        [StringLength(50)]
        public string CoachName { get; set; }

        [Required]
        public string Logo { get; set; }
        public virtual ICollection<Player> Players { get; set; }
    }
}