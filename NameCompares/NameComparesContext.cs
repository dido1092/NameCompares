using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NameCompares
{
    public class NameComparesContext : DbContext
    {
        public DbSet<BgWords>? BgWords { get; set; }
        public DbSet<BgNames>? BgNames { get; set; }
        public DbSet<EnNames>? EnNames { get; set; }
        public DbSet<PtNames>? PtNames { get; set; }
        public DbSet<LatWords>? LatWords { get; set; }
        public DbSet<WorldNames>? WorldNames { get; set; }
        public DbSet<EnBgMatchesWords>? EnBgMatchesWords { get; set; }
        public DbSet<ResultTable>? ResultTables { get; set; }
        public DbSet<LettRelations>? LettRelations { get; set; }
        public DbSet<WordTable>? WordTables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LettRelations>()
                .HasMany(e => e.Results)
                .WithOne(e => e.LettRelations)
                .HasForeignKey(e => e.LettRelationsId)
                .IsRequired();

            modelBuilder.Entity<LettRelations>()
                .HasMany(e => e.WordTables)
                .WithOne(e => e.LettRelations)
                .HasForeignKey(e => e.LettRelationsId)
                .IsRequired();

            modelBuilder.Entity<ResultTable>()
                .HasOne(e => e.LettRelations)
                .WithMany(e => e.Results)
                .HasForeignKey(e => e.LettRelationsId)
                .IsRequired();

            modelBuilder.Entity<WordTable>()
                .HasOne(e => e.LettRelations)
                .WithMany(e => e.WordTables)
                .HasForeignKey(e => e.LettRelationsId)
                .IsRequired();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Sett default connection string
                optionsBuilder.UseSqlServer(DbConfig.ConnectionString);
                optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
            base.OnConfiguring(optionsBuilder);
        }

    }
}
