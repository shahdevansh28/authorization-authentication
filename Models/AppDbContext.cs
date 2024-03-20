using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace authentication_autharization.Models
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<ShowTime> ShowTimes { get; set; }
        public DbSet<Theater> Theaters { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingOrder> BookingOrders { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
