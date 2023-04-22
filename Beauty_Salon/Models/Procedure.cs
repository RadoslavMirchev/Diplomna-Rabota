using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty_Salon.Models
{
    public class Procedure
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string Name { get; set; }
        [Required, Range(1, 1000)]
        public double Price { get; set; }
        public int Duration { get; set; } = 30;

        [ForeignKey("Worker")]
        public string WorkerId { get; set; }
        public ApplicationUser? Worker { get; set; }
        [Required, MaxLength(40)]
        public string WorkerName { get; set; }
    }
}

