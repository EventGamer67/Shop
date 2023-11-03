using Shop.Controller;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Shop.Windows
{
    /// <summary>
    /// Interaction logic for WaitingPage.xaml
    /// </summary>
    public partial class WaitingPage : Page
    {
        public Func<Task<bool>> ActionDelegate;
        Page TruePage;
        Page FalsePage;

        public WaitingPage(Func<Task<bool>> actionDelegate, Page True_Page,Page False_Page)
        {
            InitializeComponent();
            ActionDelegate = actionDelegate;
            TruePage = True_Page;
            FalsePage = False_Page;
            asdasd();
        }

        public WaitingPage()
        {
            InitializeComponent();
        }

        private async void asdasd()
        {
            bool res = await ActionDelegate.Invoke(); // Вызываем асинхронный метод
            if (res)
            {
                Manager.MainWindow.SetPageWithAnimation(TruePage);
            }
            else
            {
                Manager.MainWindow.SetPageWithAnimation(FalsePage);
            }
        }
    }
}
