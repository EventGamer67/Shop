using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class Category
    {
        public string categoryID { get; set; }
        public string category_name { get; set; }

        public Category(string categoryID, string category_name)
        {
            this.categoryID = categoryID;
            this.category_name = category_name;
        }

    }
}
