using ModernWpf;
using Shop.Controller;
using Shop.Controls;
using Shop.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using static Shop.Controls.AlertPage;

namespace Shop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            ContentView.Content = new WaitingPage();
            Manager.MainWindow = this;
            connect();
        }

        public async void connect()
        {
            bool status = await Manager.CheckServer();
            if (!status)
            {
                AlertActionDelegate reloadAction = () =>
                {
                    Manager.MainWindow.SetPageWithAnimation(new WaitingPage(
                        actionDelegate: Manager.CheckServer,
                        True_Page: new MainPage(),
                        False_Page: new AlertPage("NoConnection")
                        ));
                };

                AlertPage alertPage = new AlertPage(
                    Title: "Server not found",
                    errorMassage: "API disabled",
                    icon: "\uE7BA",
                    BtnText: "Reload",
                    actionDelegate: reloadAction
                    );

                Manager.MainWindow.SetPageWithAnimation(alertPage);
            }
            else
            {
                Manager.MainWindow.SetPageWithAnimation(new MainPage());
            }
        }

        public void SetPageWithAnimation(Page page)
        {
            DoubleAnimation timeline = new DoubleAnimation { Duration = TimeSpan.FromSeconds(0.3), To = 0.0 };
            timeline.Completed += (s, e) =>
            {
                ContentView.Content = page;
                DoubleAnimation timeline2 = new DoubleAnimation { Duration = TimeSpan.FromSeconds(0.3), To = 1.0 };
                ContentView.BeginAnimation(OpacityProperty, timeline2);
            };

            ContentView.BeginAnimation(OpacityProperty, timeline);

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var accentColor = ThemeManager.Current.ActualAccentColor;
            AccentColorBrush.Color = accentColor;

            // Создаем новый цвет, который 70% темнее акцентного цвета.
            byte newR = (byte)(accentColor.R * 0.3);
            byte newG = (byte)(accentColor.G * 0.3);
            byte newB = (byte)(accentColor.B * 0.3);

            AccentColorBrushDarker.Color = Color.FromRgb(newR, newG, newB);
        }
    }
}

/*
 Разработать CRUD систему на WPF(Магазин)
3) Добавление, изменение и удаление товаров
4) Добавление, изменение и удаление категорий
5) Поиск, сортировка и фильтрация(категория товара, цена от и до, наличие) 
7) Следующие классы: 
	Категории
	Товары
	Пользователи(Гость, Покупатель, Администратор)
8) Авторизация/Регистрация
9) Корзина

Гость:
Просмотр товаров по категориям

Авторизированный покупатель:
Добавление товаров в корзину
Оформление заказа(должно учитываться кол-во товаров)

Админ:
Добавление, изменение и удаление тоавров/категорий/производителей

 */