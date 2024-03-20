using System.ComponentModel.DataAnnotations;

namespace authentication_autharization.Models
{
    public class Booking
    {
        public long Id { get; set; }
        //public double TotalPrice { get; set; }
        [Required]
        public DateTime BookingDate { get; set; }
        [Required]
        public string Receipt { get; set; }
        public long? SeatId { get; set; }
        public Seat? Seat { get; set; }
        public long ShowTimeId { get; set; }
        public ShowTime? ShowTime { get; set; }
        /*public string UserId { get; set; }
        public virtual User? User { get; set; }*/

    }
}
