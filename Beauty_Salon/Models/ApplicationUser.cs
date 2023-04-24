using Beauty_Salon.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

public class ApplicationUser : IdentityUser
{
    [Required, MaxLength(30)]
    public string FirstName { get; set; }
    [Required, MaxLength(30)]
    public string LastName { get; set; }
    [Required, MaxLength(30)]
    public string MiddleName { get; set; }
    [Required, Range(5, 120, ErrorMessage = "Please input a valid age.")]
    public int Age { get; set; }
    public List<Appointment>? Appointments { get; set; }
    public List<Procedure> Procedures { get; set; }
}
