using System.ComponentModel.DataAnnotations;

namespace authentication_autharization.Models
{
    public class Movie
    {
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime Release_Date { get; set; }
        [Required]
        public int Duration { get; set; }
    }
}
