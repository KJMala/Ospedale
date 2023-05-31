using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Ospedale.Models
{
    public partial class ModelDbContext : DbContext
    {
        public ModelDbContext()
            : base("name=ModelDbContext")
        {
        }

        public virtual DbSet<Farmaco> Farmaco { get; set; }
        public virtual DbSet<Medico> Medico { get; set; }
        public virtual DbSet<Paziente> Paziente { get; set; }
        public virtual DbSet<Reparto> Reparto { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Visita> Visita { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medico>()
                .HasMany(e => e.Visita)
                .WithRequired(e => e.Medico)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Paziente>()
                .HasMany(e => e.Visita)
                .WithRequired(e => e.Paziente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reparto>()
                .HasMany(e => e.Farmaco)
                .WithRequired(e => e.Reparto)
                .WillCascadeOnDelete(false);
        }
    }
}
