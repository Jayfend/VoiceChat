using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Models
{
    public partial class AudioDbContext : DbContext
    {
        
        public AudioDbContext()
            : base("name=AudioDbContext")
        {
        }
        public DbSet<Audio> Audios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {   
        }
    }
}
