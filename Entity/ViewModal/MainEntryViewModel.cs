using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModal
{
    public class MainEntryViewModel
    {
        public string MainTextBoxValue { get; set; }
        public int MainDropdownValue { get; set; }
        public List<SubEntryViewModel> SubEntries { get; set; }

        public MainEntryViewModel()
        {
            SubEntries = new List<SubEntryViewModel>();
        }
    }
}
