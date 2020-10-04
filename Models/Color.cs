using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HUY_Store.Models
{
    public class Color
    {
        [Key]
        [Required]
        public string ColorId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string CSSColor { get; set; }
    }
}
