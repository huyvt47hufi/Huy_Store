using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HUY_Store.Models
{
    public class Sneaker
    {
        [Key]
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string BrandId { get; set; }
        public string Image { get; set; }
    }
}
