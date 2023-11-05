using ModernWpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
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
using Shop.Models;
using Shop.Controls;
using Shop.Views;
using Shop.Tools;
using System.Collections;
using Windows.Graphics.Printing3D;
using System.Data;
using Shop.Controller;
using static Shop.Controls.AlertPage;

namespace Shop.Windows
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        string categoryFilter = "None";
        string textFilter = "";
        private SortingTypes _sorting = SortingTypes.Name;
        public SortingTypes Sorting { get { return _sorting; } set { _sorting = value; ShowItems(); } }

        public enum SortingTypes
        {
            Price,
            Name,
        };


        public SearchPage()
        {
            InitializeComponent();
            InitData();
        }

        async private void InitData()
        {
            Loading.IsActive = true;
            bool res = await Manager.LoadData();
            if (res)
            {
                Loading.IsActive = false;
                ShowData();
            }
            else
            {
                AlertActionDelegate reloadAction = () =>
                {
                    Manager.MainWindow.SetPageWithAnimation(new WaitingPage(
                        actionDelegate: Manager.CheckServer,
                        True_Page: new SearchPage(),
                        False_Page: new AlertPage("NoConnection")
                        ));
                };
                AlertPage alertPage = new AlertPage(
                    Title:"Couldnt load data",
                    errorMassage: res.ToString(),
                    icon: "\uE7BA",
                    BtnText:"Reload",
                    actionDelegate: reloadAction
                    );
                Manager.MainWindow.SetPageWithAnimation(alertPage);
            }
        }

        private void ShowData() {
            ShowItems();
            ShowCategories();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var accentColor = ThemeManager.Current.ActualAccentColor;
            AccentColorBrush.Color = accentColor;

            //Scaffold-dbContext"connectionstring -output dir -saveFolder "
        }

        public void ShowCategories()
        {
            Filters.Items.Clear();
            Filters.Items.Add("None");
            Filters.SelectedItem = "None";
            foreach (var category in ItemViews.categories)
            {

                Filters.Items.Add(category.category_name);
            }
        }

        public void ShowItems()
        {
            //clearing
            ItemsContainer.Children.Clear();
            bool Ascendign = SortingButton.IsChecked;
            List<Item> resultList = ItemViews.items.ToList();

            //filter by category
            if (categoryFilter != null && categoryFilter != "None")
            {
                resultList = GetItemsCategoryFiltered(resultList, Tools.Tools.getCategoryID(categoryFilter));
            }

            //filter by name
            resultList = GetItemsNameFilteredItems(resultList, textFilter);

            //sorting types
            switch (Sorting)
            {
                case SortingTypes.Price:
                    resultList.Sort((a,b) => a.item_price.CompareTo(b.item_price) );
                    break;
                case SortingTypes.Name:
                    resultList.Sort((a, b) => a.item_name.CompareTo(b.item_name));
                    break;
                default:
                    break;
            }

            // asc / desc
            if (Ascendign)
            {
                mySymbolIcon.Symbol = ModernWpf.Controls.Symbol.Up;
                resultList.Reverse();
            }
            else
            {
                mySymbolIcon.Symbol = ModernWpf.Controls.Symbol.Sync;
            }

            //view
            foreach (Item item in resultList)
            {
                ItemsContainer.Children.Add(new ItemSlot(item));
            }
        }

        public List<Item> GetItemsCategoryFiltered(List<Item> items, string categoryID)
        {
            return items.Where(item => item.item_categoryID == categoryID).ToList();
        }

        public List<Item> GetItemsNameFilteredItems(List<Item> items, string filter)
        {
            if (filter != "")
            {
                return items.Where(item => item.item_name.Contains(filter)).ToList();
            }
            else
            {
                return items;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AutoSuggestBox_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            textFilter = sender.Text;
            ShowItems();
        }


        private void Filters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            categoryFilter = Filters.SelectedItem.ToString();
            ShowItems();
        }

        private void ToogleSortToPrice(object sender, RoutedEventArgs e)
        {
            Sorting = SortingTypes.Price;
        }
        private void ToogleSortToName(object sender, RoutedEventArgs e)
        {
            Sorting = SortingTypes.Name;
        }

        
        private void SortingButton_IsCheckedChanged(ModernWpf.Controls.ToggleSplitButton sender, ModernWpf.Controls.ToggleSplitButtonIsCheckedChangedEventArgs args)
        {
            ShowItems();
        }
    }
}
