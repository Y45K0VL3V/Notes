using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using yakov.Notes.Domain.Entities;
using Microsoft.Extensions.Options;

namespace yakov.Notes.Application.LocalDB
{
    public class NotesContext : DbContext
    {
        public NotesContext()
        { }

        public NotesContext(DbContextOptions<NotesContext> options) :
            base(options)
        {
            this.Database.EnsureCreated();
            //Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite();
        
        public DbSet<Note> Notes { get; set; }
    }
}
