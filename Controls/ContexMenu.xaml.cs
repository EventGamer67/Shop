using ModernWpf.Controls;
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

namespace Shop.Controls
{
    /// <summary>
    /// Interaction logic for ContexMenu.xaml
    /// </summary>
    public partial class ContexMenu : UserControl
    {
        Item context;
        Flyout flyout;
        public ContexMenu(Item item, Flyout flyout)
        {
            InitializeComponent();
            context = item;
            this.flyout = flyout;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //edit
            Manager.ShowItemEdit(context);
            flyout.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //delete
            Manager.ShowDeleteItem(context);
            flyout.Hide();
        }
    }
}
