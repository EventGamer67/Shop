using Newtonsoft.Json;
using Shop.Models;
using Shop.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Shop.Controls
{
    /// <summary>
    /// Interaction logic for NewItemPopup.xaml
    /// </summary>
    public partial class NewItemPopup : UserControl
    {
        public event EventHandler CloseDialogRequested;

        public NewItemPopup()
        {
            InitializeComponent();

            foreach (var item in ItemViews.categories)
            {
                this.category.Items.Add(item.category_name);
            }
            this.category.SelectedIndex = 0;
        }

        private void item_ImageLink_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Tools.Tools.IsUrlValid(item_ImageLink.Text))
            {
                TryLoadImage(item_ImageLink.Text);
            }
            else
            {
            }
        }

        public async void TryLoadImage(string url)
        {
            bool response = await Tools.Tools.IsImageUrl(url);
            if (response)
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(url));
                item_Image.ImageSource = bitmapImage;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //publish item
            Item item = new Item(
                itemID: ItemViews.getHighestID(),
                item_categoryID: Tools.Tools.getCategoryID(category.Text),
                item_name: item_name.Text,
                item_price: Convert.ToDouble(price.Text),
                item_image: new List<string>() { item_ImageLink.Text }
            );
            PostItem(item);
        }

        async public void PostItem(Item item)
        {
            HttpClient client = new HttpClient();
            try
            {
                var newItem = new
                {
                    itemID = item.itemID,
                    item_categoryID = item.item_categoryID,
                    item_name = item.item_name,
                    item_price = item.item_price,
                    item_image = item.item_image
                };
                MessageBox.Show(JsonConvert.SerializeObject(newItem));
                System.Net.Http.HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7080/Shop/PostItem", JsonConvert.SerializeObject(newItem));

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("PUT запрос успешно выполнен.");
                    ItemViews.items.Add(item);
                }
                else
                {
                    MessageBox.Show("Ошибка: " + response.StatusCode + " - " + response.ReasonPhrase);
                }
            }
            catch (Exception e)
            {
                Clipboard.SetText(e.ToString());
                MessageBox.Show(e.ToString());
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CloseDialogRequested?.Invoke(this, EventArgs.Empty);

        }
    }
}
