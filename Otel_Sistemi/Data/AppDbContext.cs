using Otel_Sistemi.Models;
using System.Collections.Generic;
//using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;


namespace Otel_Sistemi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomService> RoomServices { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<HotelInfo> HotelInfos { get; set; }
        public DbSet<HomeText> HomeTexts { get; set; }
        public DbSet<AboutItem> AboutItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            // Reservation ilişkileri
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Room)
                .WithMany(ro => ro.Reservations)
                .HasForeignKey(r => r.RoomId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Reservation)
                .WithOne(r => r.Payment)
                .HasForeignKey<Payment>(p => p.ReservationId);
        }
    }
}
