using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DCTTECHNOLOGIES.Model
{
    public class Register
    {
        [Key]
        [Display(Name = "User ID:")]
        public int ID { get; set; }
        [Required(ErrorMessage = "Please Enter The User Name.")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter The Password.")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Enter The Gender.")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Please Enter The Phone Number.")]
        [Display(Name = "Phone Number")]
        public Nullable<int> PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please Enter The Email.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter The Address.")]
        [Display(Name = "Address")]
        public string Address { get; set; }
    }
}