using ModernWpf.Controls;
using Shop.Controller;
using Shop.Controls;
using Shop.Models;
using Shop.Views;
using Stfu.Linq;
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
    /// Interaction logic for CategoryPage.xaml
    /// </summary>
    public partial class CategoryPage : System.Windows.Controls.Page
    {
        public CategoryPage()
        {
            InitializeComponent();
            CategoryList.ItemsSource = ItemViews.categories;
        }

        private void CategoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategoryEditPage categoryEditPage = new CategoryEditPage(e.AddedItems[0] as Category);
            CategoryEditFrame.Content = categoryEditPage;
        }

        //add category
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            //Manager.AddCategory(new Category());
            ContentDialog contentDialog = new ContentDialog();
            var itemEditPopup = new NewCategoryPopup();

            itemEditPopup.CloseDialogRequested += (s, argss) =>
            {
                contentDialog.Hide();
            };

            contentDialog.Content = itemEditPopup;
            contentDialog.ShowAsync();
        }
    }
}
