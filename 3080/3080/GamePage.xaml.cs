using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace _3080
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        int timeNow = 0;
        int maxGameTime = 5;
        int currentNoOfMoles = 0;
        int maxNoOfMoles;
        double maxAppearTime = 5;
        double currentLevel;
        Button[] holes = new Button[9];
        int[] counters = new int[9];
        DispatcherTimer timer = new DispatcherTimer();
        public GamePage()
        {
            InitializeComponent();
            currentLevel = (double) int.Parse(levelText.Text);
            maxNoOfMoles = (int) Math.Ceiling(currentLevel / 2);

            for (int i = 0; i < holes.Length; i++)
            {
                holes[i] = grid.Children[i] as Button;
                counters[i] = (int)maxAppearTime;
            }

            timer.Interval = new TimeSpan(0, 0, 1); //in Hour, Minutes, Second.
            timer.Tick += timer_Tick;

            timer.Start();
        }

        private void holeSelect()
        {
            Random rnd = new Random();
            int noOfMolesAppear = rnd.Next(0, maxNoOfMoles  + 1);
            
            for(int i = 0; i <= noOfMolesAppear; i++)
            {
                int nextHole = rnd.Next(1, holes.Length + 1) - 1;

                if (currentNoOfMoles >= maxNoOfMoles) break;
                if (!holes[nextHole].IsEnabled) holeHighlight(nextHole);

                i--;
            }
        }

        private void holeHighlight(int holeIndex)
        {
            holes[holeIndex].IsEnabled = true;
            holes[holeIndex].Background = new SolidColorBrush(Color.FromArgb(255, 48, 179, 221));

            currentNoOfMoles++;
        }

        private void holeUnhighlight(Button hole)
        {
            hole.IsEnabled = false;
            
            currentNoOfMoles--;
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            clockTime.Content = maxGameTime - timeNow;
            timeNow++;

            for(int i = 0; i < holes.Length; i++)
            {
                if (!holes[i].IsEnabled) continue;
                if(counters[i] == 0)
                {
                    App.missCount++;  
                    holeUnhighlight(holes[i]);
                    resetCounter(i);
                }

                counters[i]--;
            }

            if (currentNoOfMoles < maxNoOfMoles) holeSelect();

            if (timeNow == maxGameTime + 1)
            {
                timer.Stop();
                gameFinished();
            }
        }

        private void gameFinished()
        {
            this.NavigationService.Navigate(new Uri("GameEndPage.xaml", UriKind.Relative));
        }
 
        private void levelText_TextChanged(object sender, TextChangedEventArgs e)
        {
            levelText.Text = App.gameLevel.ToString();
        }
        
        private void resetCounter(int holeIndex)
        {
            counters[holeIndex] = (int)maxAppearTime;
        }

        private void onButtonMouseClick(object sender, RoutedEventArgs e)
        {
            Button hole = sender as Button;
            int holeIndex = int.Parse(hole.Content.ToString()) - 1;

            App.hitCount++;
            resetCounter(holeIndex);
            holeUnhighlight(hole);
        }
    }
}