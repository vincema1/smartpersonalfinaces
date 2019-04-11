using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalFinances.BUSINESS.ViewModels
{
   
    public class UserModel
    {

        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        public string UserId { get; set; }
        
      
        [Required]
        [StringLength(60, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string Name { get; set; }
      
        [Required]
        [StringLength(60, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage="please enter a valid email address")]
        public string Email { get; set; }

    }
}
