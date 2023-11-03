using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Models;

namespace Shop.Views
{
    public static class ItemViews
    {
        public static List<Item> items {get; set;}
        public static List<Item> filteredList {get; set;}
        public static List<Category> categories { get; set;}

        public static string getHighestID()
        {
            int maxItemValue = Convert.ToInt32(items.Max(item => item.itemID));
            return (maxItemValue + 1).ToString();
        }

    }
}
