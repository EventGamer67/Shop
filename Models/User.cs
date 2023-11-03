using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class User
    {
        public int userID { get; set; }
        public int user_roleID { get; set; }
        public string user_name { get; set; }
        public string user_passwordHash { get; set; }
    }
}
