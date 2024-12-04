using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Modal
{
    public class State
    {
        [Key]
        public int Id { get; set; } 
        [Required]
        public string StateName { get; set; }
    }
}
