using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GamesList.Models
{
	public class GamesModel
	{
        [Key]
		public int ID { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Genre { get; set; }
		[Required]
		public string GameStatus { get; set; }
		[Required]
		public DateTime PremiereDate { get; set; }
		[Required]
		public int DeveloperID { get; set; }

		public virtual DevModel Developer { get; set; }

        public int Hltb { get; set; }

		public int Hltb100 { get; set; }

        public string Photo { get; set; }


    }
}
