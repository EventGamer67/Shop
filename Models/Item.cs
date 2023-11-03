using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class Item
    {
        public Item(string itemID, string item_categoryID, string item_name, double item_price, List<string> item_image)
        {
            this.itemID = itemID;
            this.item_categoryID = item_categoryID;
            this.item_name = item_name;
            this.item_price = item_price;
            this.item_image = item_image;
        }

        public string itemID { get; set; }
        public string item_categoryID { get; set; }
        public string item_name { get; set; }
        public double item_price { get; set; }
        public List<string> item_image { get; set; }
    }
}
