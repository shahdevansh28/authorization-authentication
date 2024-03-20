using System.ComponentModel.DataAnnotations;

namespace authentication_autharization.Models
{
    public class BookingOrder
    {
        public int Id { get; set; }
        [Required]
        public string RazorpayOrderID { get; set; }
        public string? RazorPayPaymentId { get; set; }
        public string? Status { get; set; }
        [Required]
        public string? Receipt { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public double TotalAmount { get; set; }
        public string UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
