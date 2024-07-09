using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace DoAn.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="User cannot be blank.")]
        public string User { get; set; }


        [Required(ErrorMessage = "Password cannot be blank.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password cannot be blank.")]
        [Compare("Password",ErrorMessage ="Password and confirm password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage ="Email cannot be blank.")]
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public string Email { get; set; }

        public string Mobile { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public string City { get; set; }


    }
}