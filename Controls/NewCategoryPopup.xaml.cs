using Shop.Controller;
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

namespace Shop.Controls
{
    /// <summary>
    /// Interaction logic for NewCategoryPopup.xaml
    /// </summary>
    public partial class NewCategoryPopup : UserControl
    {
        public event EventHandler CloseDialogRequested;

        public NewCategoryPopup()
        {
            InitializeComponent();
        }

        private void CategoryName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CategoryID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //discard
            CloseDialogRequested?.Invoke(this, EventArgs.Empty);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Manager.AddCategory(new Models.Category(CategoryName.Text,CategoryID.Text));
        }
    }
}
