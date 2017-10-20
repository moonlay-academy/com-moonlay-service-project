using Com.Moonlay.EntityFrameworkCore;
using Com.Moonlay.Service.Project.Lib.Models;
using Microsoft.EntityFrameworkCore; 
using System;

namespace Com.Moonlay.Service.Project.Lib
{
    public class ProjectDbContext : BaseDbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
            : base(options)
        {            
        }

        public DbSet<Models.Project> Projects { get; set; }
        public DbSet<Backlog> Backlogs { get; set; }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
