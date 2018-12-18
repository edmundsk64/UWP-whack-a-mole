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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _3080
{
    /// <summary>
    /// Interaction logic for GameEndPage.xaml
    /// </summary>
    public partial class GameEndPage : Page
    {
        public GameEndPage()
        {
            InitializeComponent();

            ScoreText.Text = (App.hitCount / (App.hitCount + App.missCount) * 100).ToString();

        }

        private void tryAgainbutton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("StartWindow.xaml", UriKind.Relative));
        }
    }
}
