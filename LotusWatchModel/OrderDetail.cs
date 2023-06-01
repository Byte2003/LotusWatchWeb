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
	public class OrderDetail
	{
		
		public int OrderDetailId { get; set; }

		

		[Required] 
		public int Quantity { get; set;}

		public double Discount{ get; set; }

		public int OrderId { get; set; }

		[ForeignKey("OrderId")]
		[ValidateNever]
		public Order Order { get; set; }

		public int ProductId { get; set; }

		[ForeignKey("ProductId")]
		[ValidateNever] 
		public Product Product { get; set; }

	}
}
