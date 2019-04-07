using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GamesList.Models
{
	public class GameListModel
	{
		[Key]
		public int ID { get; set; }

		public int GameID { get; set; }
		[ForeignKey("GameID")]
		public GamesModel Game { get; set; }

		public int ListID { get; set; }
		[ForeignKey("ListID")]
		public ListModel List { get; set; }
	}
}
