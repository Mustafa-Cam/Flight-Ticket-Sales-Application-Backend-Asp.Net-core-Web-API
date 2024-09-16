using FlightBookingSystem.DTO;
using FlightBookingSystem.Model;
using Microsoft.EntityFrameworkCore;


namespace FlightBookingSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Tabloları tanımla
        public DbSet<Flight> Flights { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Price alanı için precision ayarı
            modelBuilder.Entity<Flight>()
                .Property(f => f.Price)
                .HasPrecision(18, 2);  // 18 basamak, 2 ondalık basamak

            base.OnModelCreating(modelBuilder);

            // İlişkiler
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Flight)
                .WithMany(f => f.Tickets)
                .HasForeignKey(t => t.FlightId);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.UserId);

        }
    }
}
