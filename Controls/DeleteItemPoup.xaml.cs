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
    /// Interaction logic for DeleteItemPoup.xaml
    /// </summary>
    public partial class DeleteItemPoup : UserControl
    {
        Item context;
        public event EventHandler CloseDialogRequested;

        public DeleteItemPoup(Item item)
        {
            InitializeComponent();
            context = item;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CloseDialogRequested?.Invoke(this, EventArgs.Empty);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Manager.deleteItem(context);
        }
    }
}
