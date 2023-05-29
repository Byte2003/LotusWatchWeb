using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusWatchModel
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }

        [Required]
        [Range(0, 1)]
        public double Discount { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }

        [Display(Name = "Brand")]
        public int BrandId { get; set; }

        [ForeignKey("BrandId")]
        [ValidateNever]
        public Brand Brand { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

    }
}
