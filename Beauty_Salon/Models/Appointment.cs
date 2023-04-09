using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty_Salon.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime AppointmentTime { get; set; }
        public int AppointmentDuration { get; set; }
        public string LocationAddress = "ул. Славянска 32, гр.Куклен, обл. Пловдив";
        [ForeignKey("Procedure")]
        public int ProcedureId { get; set; }
        public Procedure? Procedure { get; set; }
        [Required]
        public ApplicationUser Client { get; set; }
        [Required]
        public List<ApplicationUser> Workers { get; set; }
    }
}
