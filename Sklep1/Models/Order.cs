using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace Sklep1.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public ICollection<Product> Products { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? CreatedOn { get; set; }

        public string OrderStatus { get; set; }
    }
}