using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fives
{
    class DominionDbContext : DbContext
    {
        static char sep = Path.DirectorySeparatorChar;

        public DbSet<AgendaDb> Fives { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source=..{sep}..{sep}Kingdoms.db"); // TODO blbosti mezi tim
            base.OnConfiguring(optionsBuilder);
        }
    }
}
