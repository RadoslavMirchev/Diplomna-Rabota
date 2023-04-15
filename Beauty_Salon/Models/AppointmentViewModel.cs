namespace Beauty_Salon.Models
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime AppointmentTime { get; set; }
        public int Duration { get; set; }
        public int ProcedureId { get; set; }
        public Procedure? Procedure { get; set; }
    }
}
