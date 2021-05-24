using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportsIdentity.Models
{
    public class MVLogin
    {
        [Required(ErrorMessage = "*")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password, ErrorMessage = "password is invalid")]
        public string PassWord { get; set; }
    }
}