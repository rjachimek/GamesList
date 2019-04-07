using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GamesList.Models
{
	public class DevModel
	{
        [Key]
        public int ID { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Genre { get; set; }
		[Required]
		public DateTime OpenDate { get; set; }

		public DateTime ClosedDate { get; set; }
		[Required]
		public string DevStatus { get; set; }

    }
}
