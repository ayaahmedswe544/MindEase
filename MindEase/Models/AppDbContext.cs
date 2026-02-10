using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MindEase.Models
{
    public class AppDbContext:IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Memory> Memories { get; set; }
        public DbSet<MoodEntry> ModeEntries { get; set; }
        public DbSet<Journal> Journals { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Memory>().HasOne(t=>t.User).WithMany(u=>u.Memories).HasForeignKey(t=>t.UserId).OnDelete(DeleteBehavior.Cascade); 
            builder.Entity<MoodEntry>().HasOne(t => t.User).WithMany(u => u.MoodEntries).HasForeignKey(t => t.UserId).OnDelete(DeleteBehavior.Cascade); 
            builder.Entity<Journal>().HasOne(t => t.User).WithMany(u => u.Journals).HasForeignKey(t => t.UserId).OnDelete(DeleteBehavior.Cascade); 
            builder.Entity<MoodEntry>().HasIndex(m => new { m.UserId, m.Date }).IsUnique();
        }
    }
}
