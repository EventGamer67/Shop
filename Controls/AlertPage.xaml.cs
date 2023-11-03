using ModernWpf.Controls;
using Shop.Controller;
using Shop.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using Typography.OpenFont;

namespace Shop.Controls
{
    /// <summary>
    /// Interaction logic for AlertPage.xaml
    /// </summary>
    public partial class AlertPage : System.Windows.Controls.Page
    {
        public delegate void AlertActionDelegate();
        FontIcon icon = new FontIcon();
        public AlertPage(string Title, string errorMassage, string icon, string BtnText, AlertActionDelegate actionDelegate)
        {
            InitializeComponent();
            Icon.Glyph = icon;
            ErrorTitle.Content = Title;
            ErrorMsg.Content = errorMassage;
            Action.Content = BtnText;
            Action.Click += (sender, e) => actionDelegate.Invoke();
        }

        public AlertPage(string page)
        {
            InitializeComponent();

            if (page == "NoConnection")
            {
                Icon.Glyph = "\uE7BA";
                ErrorTitle.Content = "Server not found";
                ErrorMsg.Content = "API disabled";
                Action.Content = "Reload";
                Action.Click += (sender, e) => {
                    Manager.MainWindow.SetPageWithAnimation(new WaitingPage(
                        actionDelegate: Manager.CheckServer,
                        True_Page: new MainPage(),
                        False_Page: new AlertPage("NoConnection")
                        ));
                   // Manager.MainWindow.connect();
                };
            }
        }
    }
}
