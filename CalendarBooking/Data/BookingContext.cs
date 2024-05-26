using Microsoft.EntityFrameworkCore;

namespace CalendarBooking.Data
{
    public class BookingContext : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }

        public BookingContext(DbContextOptions<BookingContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional configurations if needed
        }
    }

   
}
