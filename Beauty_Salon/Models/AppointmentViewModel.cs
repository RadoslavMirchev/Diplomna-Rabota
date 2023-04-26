﻿namespace Beauty_Salon.Models
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime AppointmentTime { get; set; }
        public string? HourAndMinute { get; set; }
        public int ProcedureId { get; set; }
    }
}
