using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Shift_Scheduler.Models
{
    public class Employee
    {
        public Employee()
        {
            this.shifts = new HashSet<Shifts>();

            shiftSchedules = new List<ShiftSchedule>();

            vacationRequests = new List<Vacation>();

            clock = new List<Clock>();
        }

        [Key]
        public int employeeId { get; set; }
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Display(Name = "last Name")]
        public string lastName { get; set; }
    
        public string userName { get; set; }
      
        public string role { get; set; }

        public string address { get; set; }
        [Display(Name = "Phone Number")]
        public string phoneNumber { get; set; }

        public byte[] picture { get; set; }
        public string department { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string passWord { get; set; }

        public virtual ICollection<Shifts> shifts { get; set; }

        public virtual ICollection<ShiftChangeRequest> shiftChangeRequest { get; set; }
        public virtual ICollection<ShiftSchedule> shiftSchedules { get; set; }
        public virtual ICollection<Vacation> vacationRequests { get; set; }
        public virtual ICollection<Clock> clock { get; set; }


    }
}