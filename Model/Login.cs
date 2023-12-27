using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DCTTECHNOLOGIES.Model
{
    public class Login
    {
        [Required(ErrorMessage = "Please Enter The Email.")]
        [Display(Name = "Email:")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter The Password.")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}