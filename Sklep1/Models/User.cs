using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sklep1.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, Column(TypeName = "VARCHAR")]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public String Password { get; set; }

        [EmailAddress, Index(IsUnique = true), Column(TypeName = "VARCHAR"), Required]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Picture")]
        public string ImageUrl { get; set; }

    }
}