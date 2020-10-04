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
        [Required]
        public string ItemId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Size { get; set; }
        [Required]
        public string ColorId { get; set; }
        [Required]
        public string BrandId { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public int State { get; set; }
    }
}
