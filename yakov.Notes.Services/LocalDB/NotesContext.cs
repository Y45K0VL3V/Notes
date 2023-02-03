using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yakov.Notes.Domain.Entities;

namespace yakov.Notes.Services.LocalDB
{
    public class NotesContext : DbContext
    {
        public NotesContext()
        {
            //this.Database.Migrate();
            this.Database.EnsureCreated();
        }

        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite("Data Source=Notes.db");
        }
    }
}
