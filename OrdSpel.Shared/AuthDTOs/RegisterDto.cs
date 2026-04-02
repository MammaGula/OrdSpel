using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OrdSpel.Shared.AuthDTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "Lösenordet måste vara minst 4 tecken.")]
        public string Password { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "Lösenordet måste vara minst 4 tecken.")]
        [Compare("Password", ErrorMessage = "Lösenorden matchar inte.")]
        public string ConfirmPassword { get; set; }
    }
}
