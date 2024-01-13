using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TESTMVCCORE.Models.DB
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Persona> Persona { get; set; } = null!;
        public virtual DbSet<PersonaDetail> PersonaDetail { get; set; } = null!;
        public virtual DbSet<Persona_> Persona_ { get; set; } = null!;
        public virtual DbSet<TESTTHETABLE> TESTTHETABLE { get; set; } = null!;
        public virtual DbSet<Type> Type { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>(entity =>
            {
                entity.Property(e => e.Type).IsFixedLength();
            });

            modelBuilder.Entity<Persona_>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TESTTHETABLE>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).IsFixedLength();

                entity.Property(e => e.Number).IsFixedLength();
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.Property(e => e.Type1).IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
