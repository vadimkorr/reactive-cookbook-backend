using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace reactive.Models.DTO
{
    public class UserRegisterDto
    {
        //[Required(ErrorMessage = "Login is required")]
        public string Email;
        public string UserName;
        //[Range(3, 10, ErrorMessage = "Password's length must be in [3, 10]")]
        //[Required(ErrorMessage = "Password is required")]
        public string Password;
    }
}
