using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Aruba.Eis.Models;
using System.Data.Entity;
using Aruba.Eis.Models.Entities;
using System.Threading.Tasks;

namespace Aruba.Eis.EntityFramework
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private string _username;

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public ApplicationDbContext(string username)
            : base("name=DefaultConnection", throwIfV1Schema: false)
        {
            if (username == null)
                username = "Anonymous";
            else
                _username = username;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<TeamEntity> DbTeams { get; set; }

        public virtual DbSet<ActivityEntity> DbActivities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Add Team-TeamResource One to many relationship
            modelBuilder.Entity<TeamResourceEntity>()
                .HasRequired<TeamEntity>(tr => tr.Team)
                .WithMany(t => t.Resources)
                .HasForeignKey<int>(tr => tr.TeamId)
                .WillCascadeOnDelete(false);

            // Add User-TeamResource One to many relationship
            modelBuilder.Entity<TeamResourceEntity>()
                .HasRequired<ApplicationUser>(tr => tr.User)
                .WithMany(u => u.TeamResources)
                .HasForeignKey<string>(tr => tr.UserId)
                .WillCascadeOnDelete(false);

            // Add Activity-ActivityResource One to many relationship
            modelBuilder.Entity<ActivityResourceEntity>()
                .HasRequired<ActivityEntity>(ar => ar.Activity)
                .WithMany(a => a.Resources)
                .HasForeignKey<int>(ar => ar.ActivityId)
                .WillCascadeOnDelete(true);

            // Add Schedule-ScheduleResource One to many relationship
            modelBuilder.Entity<ScheduleResourceEntity>()
                .HasRequired<ScheduleEntity>(sr => sr.Schedule)
                .WithMany(s => s.Resources)
                .HasForeignKey<int>(sr => sr.ScheduleId)
                .WillCascadeOnDelete(false);

            // Add Schedule-Assignment One to many relationship
            modelBuilder.Entity<AssignmentEntity>()
                .HasRequired<ScheduleEntity>(a => a.Schedule)
                .WithMany(s => s.Assignments)
                .HasForeignKey<int>(a => a.ScheduleId)
                .WillCascadeOnDelete(false);
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).DateCreated = DateTime.UtcNow;
                    ((BaseEntity)entity.Entity).UserCreated = _username;
                }

                ((BaseEntity)entity.Entity).DateModified = DateTime.UtcNow;
                ((BaseEntity)entity.Entity).UserModified = _username;
            }
        }
    }
}