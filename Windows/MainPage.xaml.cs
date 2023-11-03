using ModernWpf.Controls;
using Newtonsoft.Json;
using Shop.Models;
using Shop.Views;
using System;
using System.Collections.Generic;
using System.Linq;
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
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using Shop.Models;
using Shop.Controller;
using Shop.Controls;
using static Shop.Controls.AlertPage;

namespace Shop.Windows
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        

        //login
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if(LoginTB.Text == "" || PasswordTB.Text == "")
            {
                return;
            }
            UserDto userDto = new UserDto();
            userDto.user_name = LoginTB.Text;
            userDto.password = PasswordTB.Text;

            bool result = await Manager.Login(userDto);

            if (result)
            {
                Manager.MainWindow.SetPageWithAnimation(new SecondPage());
            }
            else
            {
                Manager.ShowAlert("Error");
            }

            //mainWindow.SetPageWithAnimation(new SecondPage());
        }

        //create
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (LoginTB.Text == "" || PasswordTB.Text == "")
            {
                return;
            }
            UserDto userDto = new UserDto();
            userDto.user_name = LoginTB.Text;
            userDto.password = PasswordTB.Text;

            bool result = await Manager.Register(userDto);

            if (result)
            {
                Manager.MainWindow.SetPageWithAnimation(new SecondPage());
            }
            else
            {
                Manager.ShowAlert("Error");
            }
        }

        
    }
}
