using Newtonsoft.Json;
using Shop.Models;
using Shop.Tools;
using Shop.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
using Windows.Devices.PointOfService;
using Windows.Foundation.Metadata;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using Shop.Controller;

namespace Shop.Controls
{
    /// <summary>
    /// Interaction logic for ItemEditPopup.xaml
    /// </summary>
    public partial class ItemEditPopup : UserControl
    {
        public event EventHandler CloseDialogRequested;
        Item context;
        public ItemEditPopup(Item context)
        {
            InitializeComponent();
            this.context = context;

            BitmapImage bitmapImage = new BitmapImage(new Uri(context.item_image[0]));
            this.item_Image.ImageSource = bitmapImage;

            this.item_ImageLink.Text = context.item_image[0].ToString();

            this.item_name.Text = context.item_name;

            this.price.Text = context.item_price.ToString();

            foreach (var item in ItemViews.categories)
            {
                this.category.Items.Add(item.category_name);
            }
            this.category.SelectedItem = ItemViews.categories.Where(cat => cat.categoryID == context.item_categoryID).ToList()[0].category_name;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //save
            updateItem();
        }

        async private void updateItem()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Manager.token);
            try
            {
                var newItem = new
                {
                    itemID = context.itemID,
                    item_categoryID = Tools.Tools.getCategoryID(this.category.Text),
                    item_name = this.item_name.Text,
                    item_price = Convert.ToDouble(this.price.Text),
                    item_image = new List<string> { this.item_ImageLink.Text }
                };
                
                System.Net.Http.HttpResponseMessage response = await client.PutAsJsonAsync("https://localhost:7080/Shop/UpdateItem",JsonConvert.SerializeObject(newItem));

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("PUT запрос успешно выполнен.");
                }
                else
                {
                    MessageBox.Show("Ошибка: " + response.StatusCode + " - " + response.ReasonPhrase);
                    MessageBox.Show(response.Headers.ToString());
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



