﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModernWpf.Controls;
using Newtonsoft.Json;
using Shop.Controls;
using Shop.Models;
using Shop.Properties;
using Shop.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Windows.ApplicationModel.AppExtensions;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Security.Authentication.Web.Core;
using Windows.System;
using Windows.Web.Http;
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
            try
            {
                System.Net.Http.HttpResponseMessage response = await HttpClientSingletone.Instance().PostAsJsonAsync($"{Settings.Default.APIURL}addcategory", category);
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
                System.Net.Http.HttpResponseMessage response = await HttpClientSingletone.Instance().DeleteAsync($"{Settings.Default.APIURL}deleteitem/{item.itemID}");

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

        public async static Task<bool> DeleteCategory(Category category)
        {
            try
            {
                System.Net.Http.HttpResponseMessage response = await HttpClientSingletone.Instance().DeleteAsync($"{Settings.Default.APIURL}deletecategory/{category.categoryID}");

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("DELETE запрос успешно выполнен.");
                    ItemViews.categories.Remove(category);
                    return true;
                }
                else
                {
                    MessageBox.Show("Ошибка: " + response.StatusCode + " - " + response.ReasonPhrase);
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }

        public async static Task<bool> Login(UserDto userDto)
        {
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            JsonContent jsonContent = JsonContent.Create(userDto);
            System.Net.Http.HttpResponseMessage response = await httpClient.PostAsync($"{Settings.Default.APIURL}login", jsonContent);

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
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            JsonContent jsonContent = JsonContent.Create(userDto);
            System.Net.Http.HttpResponseMessage response = await httpClient.PostAsync($"{Settings.Default.APIURL}register", jsonContent);

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
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            try
            {
                System.Net.Http.HttpResponseMessage response = await httpClient.GetAsync($"{Settings.Default.APIURL}ping");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
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

        public async static Task<bool> LoadData()
        {
            try
            {
                System.Net.Http.HttpResponseMessage response = await HttpClientSingletone.Instance().GetAsync($"{Settings.Default.APIURL}getcategories");

                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    try
                    {
                        List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(jsonContent);
                        ItemViews.categories = categories;
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

                response = await HttpClientSingletone.Instance().GetAsync($"{Settings.Default.APIURL}getitems");

                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    try
                    {
                        List<Item> items = JsonConvert.DeserializeObject<List<Item>>(jsonContent);
                        Shop.Views.ItemViews.items = items;
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
                return true;

            }
            catch
            {
                return false;
            }
        }

        public async static Task<string> EditCategory(Category category)
        {
            try
            {
                //string content = JsonConvert.SerializeObject(category);
                System.Net.Http.HttpResponseMessage response = await HttpClientSingletone.Instance().PutAsJsonAsync($"{Settings.Default.APIURL}updatecategory", category);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("edited добавлена");
                    ItemViews.categories.Where(cat => cat.categoryID == category.categoryID).ToList()[0].category_name = category.category_name;
                    return "ok";
                }
                else
                {
                    MessageBox.Show("Ошибка: " + response.StatusCode + " - " + response.ReasonPhrase);
                    return "err";

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return "err";
            }
        }

        public async static Task<bool> UpdateItem(string jsoncontext)
        {
            try
            {
                System.Net.Http.HttpResponseMessage response = await HttpClientSingletone.Instance().PutAsJsonAsync($"{Settings.Default.APIURL}UpdateItem", jsoncontext);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("PUT запрос успешно выполнен.");
                    return true;
                }
                else
                {
                    MessageBox.Show("Ошибка: " + response.StatusCode + " - " + response.ReasonPhrase);
                    MessageBox.Show(response.Headers.ToString());
                    return false;
                }
            }
            catch (Exception e)
            {
                Clipboard.SetText(e.ToString());
                MessageBox.Show(e.ToString());
                return false;
            }
        }
    }
}