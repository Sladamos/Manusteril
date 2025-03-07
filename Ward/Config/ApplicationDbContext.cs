﻿using Ward.Visit;
using Ward.Patient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Room;
using Ward.Visit.Question;

namespace Ward.Config
{
    internal partial class ApplicationDbContext : DbContext
    {
        private readonly DbConfig config;

        public DbSet<VisitEntity> Visits { get; set; }

        public DbSet<PatientEntity> Patients { get; set; }

        public DbSet<RoomEntity> Rooms { get; set; }

        public DbSet<VisitQuestionEntity> Questions { get; set; }

        public ApplicationDbContext(DbConfig config, DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            this.config = config;
        }

        public void Ensure()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseNpgsql(config.ConnectionString);
            }
        }
    }
}
