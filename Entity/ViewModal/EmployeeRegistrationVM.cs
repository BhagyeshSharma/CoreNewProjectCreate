using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Entity.ViewModal
{
    public class EmployeeRegistrationVM
    {
        public int EmployeeId { get; set; }
        [Display(Name ="Employee Name")]
        public string EmployeeName { get; set; }
        [Display(Name = "Employee Father Name")]
        public string EmpFatherName { get; set; }
        [Display(Name = "State Name")]
        public int StateId { get; set; }
        [Display(Name = "State Name")]
        public string StateName { get; set; }
        [Display(Name ="Employee Document")]
        public IFormFile FileUpload { get; set; }
        public string varfile { get; set; }
    }
}
