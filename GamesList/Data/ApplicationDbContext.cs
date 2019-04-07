using System;
using System.Collections.Generic;
using System.Text;
using GamesList.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GamesList.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

        public DbSet<GamesModel> Games { get; set; }
        public DbSet<DevModel> Devs { get; set; }
        public DbSet<ListModel> Lists { get; set; }
        public DbSet<GamesList.Models.GameListModel> GameListModel { get; set; }
    }
}
