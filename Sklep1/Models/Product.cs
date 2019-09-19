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
        [Display(Name = "Cena")]
        public int Price { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [MinLength(10), Required]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [MinLength(3), Required]
        [Display(Name = "Kategoria")]
        public string Category { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Zdjęcie")]
        public string ImageUrl { get; set; }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}