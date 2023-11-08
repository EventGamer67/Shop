using Shop.Controller;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Shop.Windows
{
    /// <summary>
    /// Interaction logic for CategoryEditPage.xaml
    /// </summary>
    public partial class CategoryEditPage : Page
    {
        Category context;
        public CategoryEditPage(Category category)
        {
            InitializeComponent();
            this.context = category;
            category_name.Text = context.category_name;
            categoryID.Text = context.categoryID;
        }

        //save
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await Manager.EditCategory(new Category(context.categoryID, category_name.Text));
        }

        //delete
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Manager.DeleteCategory(context);
        }
    }
}