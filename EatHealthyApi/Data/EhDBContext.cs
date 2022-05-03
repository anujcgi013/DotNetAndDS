using EatHealthyApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace EatHealthyApi.Data
{
    public class EhDBContext : IdentityDbContext
    {
        public EhDBContext(DbContextOptions<EhDBContext> opt) : base(opt)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>(entity =>
            {
                entity.ToTable("ApplicationUser");
                entity.Property(p => p.Id).HasColumnName("User_Id");
            });
            modelBuilder.Entity<IdentityRole>().ToTable("ApplicationRole");
        }
     public DbSet<ApplicationUser> EHUser { get; set; }
     public DbSet<DislikeItems> DislikeItems { get; set; }
     public DbSet<FoodPreferences> FoodPreference { get; set; }
     public DbSet<Goals> Goals { get; set; }
     public DbSet<ActivityType> ActivityType { get; set; }
     public DbSet<VarietyType> VarietyType { get; set; }
    }
}