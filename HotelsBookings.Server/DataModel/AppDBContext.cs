using Microsoft.EntityFrameworkCore;

namespace HotelsBookings.Server.DataModel
{
    public class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships, if not configured via conventions
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Hotel)
                .WithMany()
                .HasForeignKey(b => b.HotelsId);

            // Other configurations...

            base.OnModelCreating(modelBuilder);
        }


    }

}
