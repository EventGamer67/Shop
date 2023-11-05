using ModernWpf.Controls;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
using Windows.UI.Composition;

namespace Shop.Controls
{
    /// <summary>
    /// Interaction logic for ItemSlot.xaml
    /// </summary>
    public partial class ItemSlot : UserControl
    {
        Item context;

        public ItemSlot(Item item)
        {
            InitializeComponent();
            this.context = item;

        }

        public ItemSlot(Item item, List<string> permissions)
        {
            InitializeComponent();
            this.context = item;

            if (permissions.Contains("Edit"))
            {

            }
            if (permissions.Contains("Delete"))
            {

            }
            if (permissions.Contains("Buy"))
            {

            }
        }

        private void LoadImage()
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri(context.item_image[0]));
            this.BgImage.ImageSource = bitmapImage;
        } 

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ItemName.Text = context.item_name;
            LoadImage();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Flyout flyout = new Flyout();
            flyout.Content = new ContexMenu(context, flyout);
            flyout.ShowAt((FrameworkElement)sender);
        }
    }
}
