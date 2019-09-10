using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sklep1.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Price { get; set; }

        public string Name { get; set; }

        [MinLength(10), Required]
        public string Description { get; set; }

        [MinLength(3), Required]
        public string Category { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Picture")]
        public string ImageUrl { get; set; }
    }
}