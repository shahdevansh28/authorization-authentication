using System.ComponentModel.DataAnnotations;

namespace authentication_autharization.Models
{
    public class Theater
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        //[Required]
        //public int Capacity { get; set; }
    }
}
