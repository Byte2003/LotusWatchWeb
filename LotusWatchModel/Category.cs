using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusWatchModel
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [ValidateNever]
        public ICollection<Product> Products { get; set; }

        [ValidateNever]
        public ICollection<Brand> Brands { get; set; }


    }
}
