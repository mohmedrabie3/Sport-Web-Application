using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SportsIdentity.Models
{
    public class Player
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int DateOfBirth { get; set; }

        [Required]
        [StringLength(50)]
        public string national { get; set; }

        [Required]
        public string image { get; set; }
        public int IDTeam { get; set; }
       
        public virtual FootBallTeam FootBallTeam { get; set; }
       
    }
}