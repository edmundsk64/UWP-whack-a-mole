using System;
using System.Windows;

namespace _3080
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NavigateToSettingPage();
           
        }

        void NavigateToSettingPage()
        {
            MainFrame.Navigate(new Uri("SettingPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
