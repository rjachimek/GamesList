using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GamesList.Models
{
	public class ListModel
	{
        [Key]
        public int ListID { get; set; }
        public string Status { get; set; }
        public string UserID { get; set; }
		[ForeignKey("UserID")]
		public IdentityUser User { get; set; }



    }
}
