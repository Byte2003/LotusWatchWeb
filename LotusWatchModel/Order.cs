using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusWatchModel
{
	public class Order
	{
		[Key]
		public int OrderId { get; set; }

		[Required]
		public int AppUserId { get; set; }

		[Required] 
		public DateTime OrderDate { get; set; }

		[Required]
		public DateTime ShippedDate { get; set; }

		[Required]
		public string ShipName { get; set; }

		[Required]
		public string ShipAddress { get; set; }

		[Required]
		public decimal OrderTotal { get; set; }

	}
}
