using ModernWpf.Controls;
using Newtonsoft.Json;
using Shop.Controls;
using Shop.Models;
using Shop.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Security.Authentication.Web.Core;
using Windows.System;
using User = Shop.Models.User;

namespace Shop.Controller
{
    static public class Manager
    {
        public static User User { get; set; }

        public static MainWindow MainWindow { get; set; }

        public static string token { get; set; }

        public static void ShowItemEdit(Item context)
        {
            ContentDialog contentDialog = new ContentDialog();
            var itemEditPopup = new ItemEditPopup(context);

            itemEditPopup.CloseDialogRequested += (s, args) =>
            {
                contentDialog.Hide();
            };

            contentDialog.Content = itemEditPopup;
            contentDialog.ShowAsync();
        }

        public static void ShowDeleteItem(Item context)
        {
            ContentDialog contentDialog = new ContentDialog();
            var itemEditPopup = new DeleteItemPoup(context);
            contentDialog.Content = itemEditPopup;

            itemEditPopup.CloseDialogRequested += (s, args) =>
            {
                contentDialog.Hide();
            };

            contentDialog.ShowAsync();
        }

        public async static void AddCategory(Category category)
        {
            HttpClient client = new HttpClient();
            try
            {
                string content = JsonConvert.SerializeObject(category);
                System.Net.Http.HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7080/Shop/AddCategory", content);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("категория добавлена");
                    ItemViews.categories.Add(category);
                }
                else
                {
                    MessageBox.Show("Ошибка: " + response.StatusCode + " - " + response.ReasonPhrase);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public async static void deleteItem(Item item)
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
                System.Net.Http.HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7080/Shop/{item.itemID}");

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("DELETE запрос успешно выполнен.");
                    ItemViews.items.Remove(item);
                }
                else
                {
                    MessageBox.Show("Ошибка: " + response.StatusCode + " - " + response.ReasonPhrase);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public async static Task<bool> Login(UserDto userDto)
        {
            HttpClient client = new HttpClient();
            JsonContent jsonContent = JsonContent.Create(userDto);
            System.Net.Http.HttpResponseMessage response = await client.PostAsync("https://localhost:7080/login", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                //(Models.User user, token) = await response.Content.ReadFromJsonAsync<(Models.User, string)>();
                Tuple<User, string> data = await response.Content.ReadFromJsonAsync<Tuple<User, string>>();
                Manager.User = data.Item1;
                Manager.token = data.Item2;
                return true;
            }
            else
            {
                return false;
            }
        }

        public async static Task<bool> Register(UserDto userDto)
        {
            HttpClient client = new HttpClient();
            JsonContent jsonContent = JsonContent.Create(userDto);
            System.Net.Http.HttpResponseMessage response = await client.PostAsync("https://localhost:7080/register", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                (Models.User user, token) = await response.Content.ReadFromJsonAsync<(Models.User, string)>();
                Manager.User = user;
                return true;
            }
            else
            {
                return false;
            }
        }

        public async static Task<bool> CheckServer()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync("https://localhost:7080/api/Service/ping");

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (HttpRequestException)
            {
                return false;
            }
        } 

        public static void ShowAlert(string message)
        {
            ContentDialog dialog = new ContentDialog();
            var item = new AlertPopup(message);
            item.CloseDialogRequested += (s, argss) =>
            {
                dialog.Hide();
            };
            dialog.Content = item;
            dialog.ShowAsync();
        }
    }
}
