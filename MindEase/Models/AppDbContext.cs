using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MindEase.Models
{
    public class AppDbContext:IdentityDbContext<GeneralUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Memory> Memories { get; set; }
        public DbSet<MoodEntry> ModeEntries { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<User> AppUsers { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Memory>().HasOne(t=>t.User).WithMany(u=>u.Memories).HasForeignKey(t=>t.UserId).OnDelete(DeleteBehavior.Cascade); 
            builder.Entity<MoodEntry>().HasOne(t => t.User).WithMany(u => u.MoodEntries).HasForeignKey(t => t.UserId).OnDelete(DeleteBehavior.Cascade); 
            builder.Entity<Journal>().HasOne(t => t.User).WithMany(u => u.Journals).HasForeignKey(t => t.UserId).OnDelete(DeleteBehavior.Cascade); 
            builder.Entity<MoodEntry>().HasIndex(m => new { m.UserId, m.Date }).IsUnique();

            builder.Entity<DoctorSessionSlot>()
        .HasIndex(s => new { s.DoctorId, s.StartDateTime })
        .IsUnique();

            builder.Entity<DoctorSessionSlot>()
                .Property(s => s.SlotStatus)
                .HasConversion<string>();

            builder.Entity<Booking>()
                .Property(b => b.BookingStatus)
                .HasConversion<string>();

          builder.Entity<Booking>()
            .HasIndex(b => b.DoctorSessionSlotId)
              .IsUnique();

            builder.Entity<Booking>()
        .HasOne(b => b.DoctorSessionSlot)
        .WithOne(s => s.Booking)
        .HasForeignKey<Booking>(b => b.DoctorSessionSlotId)
        .OnDelete(DeleteBehavior.Restrict); 

            
            builder.Entity<Booking>()
                .HasOne(b => b.Doctor)
                .WithMany(d => d.Bookings)
                .HasForeignKey(b => b.DoctorId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.Entity<DoctorSessionSlot>()
                .HasOne(s => s.Doctor)
                .WithMany(d => d.SessionSlots)
                .HasForeignKey(s => s.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Memory>()
                .Property(m => m.MoodState)
                .HasConversion<string>();

            builder.Entity<MoodEntry>()
               .Property(m => m.MoodType)
               .HasConversion<string>();

            builder.Entity<User>().Property(u=>u.Gender).HasConversion<string>();

            builder.Entity<Doctor>()
                   .HasIndex(d => d.LicenseNumber)
                   .IsUnique();

            builder.Entity<UserDoctor>()
                .HasKey(ud => new { ud.UserId, ud.DoctorId }); 

            builder.Entity<UserDoctor>()
                .HasOne(ud => ud.User)
                .WithMany(u => u.UserDoctors)
                .HasForeignKey(ud => ud.UserId).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<UserDoctor>()
                .HasOne(ud => ud.Doctor)
                .WithMany(d => d.UserDoctors)
                .HasForeignKey(ud => ud.DoctorId).OnDelete(DeleteBehavior.NoAction);

            //ch
            builder.Entity<Chat>()
               .HasOne(c => c.Booking)
               .WithOne(b=>b.Chat)
               .HasForeignKey<Chat>(c => c.BookingId)
               .OnDelete(DeleteBehavior.Cascade);
               
            builder.Entity<ChatMessage>()
                 .HasOne(m => m.Chat)
                 .WithMany(c => c.Messages)
                 .HasForeignKey(m => m.ChatId)
                 .OnDelete(DeleteBehavior.Cascade);
         }
    }
}
