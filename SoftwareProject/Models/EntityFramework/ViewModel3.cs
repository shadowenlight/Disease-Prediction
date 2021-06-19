using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoftwareProject.Models.EntityFramework
{
    public class ViewModel3
    {   [Required]
        public string PatientName { get; set; }
        public string PatientEmail{ get; set; }
        public string PatientSurname { get; set; }
        public int PatientId { get; set; }
        public Op_Table operations { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string IllnessName { get; set; }
        public int IllnessId { get; set; }

        public string AppName { get; set; }
        [Required(ErrorMessage = "Appointment date is required")]
        public DateTime AppDate { get; set; }
        [Required(ErrorMessage = "Hospital is required")]
        public string HospitalName { get; set; }
        [Required(ErrorMessage = "Appointment time is required")]
        public string DappTime { get; set; }
        [Required(ErrorMessage = "Department is required")]
        public string DepartmentName { get; set; }
        [Required(ErrorMessage = "Doctor is required")]
        public string DoctorName { get; set; }



    }
}