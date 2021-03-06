﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Shift_Scheduler.Models
{
    public class Vacation
    {

        [Key]
        public int vacationID { get; set; }

        public DateTime dateStart { get; set; }

        public DateTime dateEnd { get; set; }

        public string approvalStatus { get; set; }

        public int employeeId { get; set; }

        [ForeignKey("employeeId")]
        public virtual Employee Employees { get; set; }

    }
}