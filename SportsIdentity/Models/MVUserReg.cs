using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportsIdentity.Models
{
    public class MVUserReg
    {
        [Required(ErrorMessage = "*")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password, ErrorMessage = "password is invalid")]
        public string PassWord { get; set; }
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password , ErrorMessage ="password is invalid")]
        [Compare("PassWord",ErrorMessage ="Password is not a match")]
        public string ConfirmPassWord { get; set; }
        [Required(ErrorMessage = "*")]
        [DataType(DataType.EmailAddress , ErrorMessage ="Email is invalid")]
        public string Email { get; set; }
        [Required(ErrorMessage ="*")]
        public string Address { get; set; }


    }
}