using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace DoAn.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage = "User cannot be blank.")]
        public string User { get; set; }


        [Required(ErrorMessage = "Password cannot be blank.")]
        public string Password { get; set; }
    }
}