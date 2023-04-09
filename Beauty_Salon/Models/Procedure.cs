using System.ComponentModel.DataAnnotations;

namespace Beauty_Salon.Models
{
    public class Procedure
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        public int Duration { get; set; } = 30;

    }
}

