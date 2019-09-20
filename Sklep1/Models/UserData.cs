using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sklep1.Models
{
    public class UserData
    {
        [Key] public int Id { get; set; }

        public string Imie { get; set; }

        public string Nazwisko { get; set; }

        public string Email { get; set; }

        public string Ulica { get; set; }

        [Display(Name = "Numer domu")] public int Numerdomu { get; set; }

        [Display(Name = "Kod pocztowy")] public int Kodpocztowy { get; set; }

        public string Miasto { get; set; }
    }
}