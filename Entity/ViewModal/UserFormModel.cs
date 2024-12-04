using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModal
{
    public class UserFormModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public UserAddressModel UserAddressModel { get; set; }
        public UserContactModel UserContactModel { get; set; }
        public List<UserFormModel> Userlist { get; set; }
    }
}
