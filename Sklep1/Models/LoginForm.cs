using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sklep1.Models
{
    public class LoginForm
    {
        public string Username { get; set; }
        public String Password { get; set; }
    }
}