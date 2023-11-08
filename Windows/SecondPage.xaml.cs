using ModernWpf;
using ModernWpf.Controls;
using Shop.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Interaction logic for SecondPage.xaml
    /// </summary>
    public partial class SecondPage : System.Windows.Controls.Page
    {
        public SecondPage()
        {
            InitializeComponent();
        }

        private void NavBar_SelectionChanged(ModernWpf.Controls.NavigationView sender, ModernWpf.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            string page = args.SelectedItemContainer.Tag.ToString();

            if("HomePage" == page)
            {
                ContentFrame.Content = new SearchPage();
            }
            if("Add" == page)
            {
                //new item popup
                ContentDialog contentDialog = new ContentDialog();
                var itemEditPopup = new NewItemPopup();

                itemEditPopup.CloseDialogRequested += (s, argss) =>
                {
                    contentDialog.Hide();
                };

                contentDialog.Content = itemEditPopup;
                contentDialog.ShowAsync();
            }
            if("AddCategory" == page)
            {
                ContentFrame.Content = new CategoryPage();

                
            }
        }
    }
}
