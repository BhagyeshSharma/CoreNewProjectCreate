using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Entity.ViewModal
{
    public class UploadViewModel
    {
        public int DivisionId { get; set; }
        public int DistrictId { get; set; }
        public int BlockId { get; set; }

        public IEnumerable<SelectListItem> Divisions { get; set; }
        public IEnumerable<SelectListItem> Districts { get; set; }
        public IEnumerable<SelectListItem> Blocks { get; set; }

        [Required]
        public IFormFile UploadedFile { get; set; }
    }
}
