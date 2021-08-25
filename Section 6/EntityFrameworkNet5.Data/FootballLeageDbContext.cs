﻿using EntityFrameworkNet5.Data.Configurations.Entities;
using EntityFrameworkNet5.Domain;
using EntityFrameworkNet5.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EntityFrameworkNet5.Data
{
    public class FootballLeageDbContext : AuditableFootballLeageDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=FootballLeage_EfCore")
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information )
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>()
                .HasMany(m => m.HomeMatches)
                .WithOne(m => m.HomeTeam)
                .HasForeignKey(m => m.HomeTeamId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Team>()
                .HasMany(m => m.AwayMatches)
                .WithOne(m => m.AwayTeam)
                .HasForeignKey(m => m.AwayTeamId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TeamsCoachesLeaguesView>().HasNoKey().ToView("TeamsCoachesLeagues");

            modelBuilder.ApplyConfiguration(new LeagueSeedConfiguration());
            modelBuilder.ApplyConfiguration(new TeamSeedConfiguration());
            modelBuilder.ApplyConfiguration(new CoachSeedConfiguration());


        }

        
        public DbSet<Team> Teams { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<TeamsCoachesLeaguesView> TeamsCoachesLeagues { get; set; }

    }
}
