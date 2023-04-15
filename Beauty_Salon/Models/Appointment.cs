using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty_Salon.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime AppointmentDate { get; set; }
        [Required]
        public DateTime AppointmentTime { get; set; }
        public string HourAndMinute { get; set; }
        public int Duration { get; set; } = 30;
        [ForeignKey("Procedure")]
        public int ProcedureId { get; set; }
        public Procedure? Procedure { get; set; }
        [Required]
        public string ProcedureName { get; set; }
        [Required]
        public string WorkerName { get; set; }
    }
}
