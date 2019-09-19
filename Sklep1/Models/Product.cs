using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Sklep1.Models
{
    public class Product : IEnumerable
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

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}