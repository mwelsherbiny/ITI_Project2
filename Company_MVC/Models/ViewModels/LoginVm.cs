using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Company_MVC.Models.ViewModels
{
    public class LoginVm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
