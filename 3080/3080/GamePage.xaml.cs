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
        private const int V = 0;
        private int timeNow = 0;
        private readonly int maxGameTime = 30;
        private int currentNoOfMoles = V;
        private readonly int maxNoOfMoles;
        private readonly double maxAppearTime = 5;
        private readonly double currentLevel;

        public DispatcherTimer Timer { get; } = new DispatcherTimer();

        public Button[] Holes { get; } = new Button[9];

        public int[] Counters { get; } = new int[9];

        public GamePage()
        {
            InitializeComponent();
            currentLevel = int.Parse(levelText.Text);
            maxNoOfMoles = (int)Math.Ceiling(currentLevel / 2);

            for (int i = 0; i < Holes.Length; i++)
            {
                Holes[i] = (Button)grid.Children[i];
                Counters[i] = (int)maxAppearTime;
            }

            Timer.Interval = new TimeSpan(0, 0, 1); //in Hour, Minutes, Second.
            Timer.Tick += Timer_Tick;

            Timer.Start();
        }

        private void HoleSelect()
        {
            Random rnd = new Random();
            int noOfMolesAppear = rnd.Next(0, maxNoOfMoles + 1);

            for (int i = noOfMolesAppear; i >= 0; i--)
            {
                int nextHole = rnd.Next(1, Holes.Length + 1) - 1;

                if (currentNoOfMoles >= maxNoOfMoles) break;
                if (!Holes[nextHole].IsEnabled) HoleHighlight(nextHole);

                i--;
            }
        }

        private void HoleHighlight(int holeIndex)
        {
            Holes[holeIndex].IsEnabled = true;
            Holes[holeIndex].Background = new SolidColorBrush(Color.FromArgb(255, 48, 179, 221));

            currentNoOfMoles++;
        }

        private void HoleUnhighlight(Button hole)
        {
            hole.IsEnabled = false;
            
            currentNoOfMoles--;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            clockTime.Content = maxGameTime - timeNow;
            timeNow++;

            for(int i = 0; i < Holes.Length; i++)
            {
                if (!Holes[i].IsEnabled) continue;
                if(Counters[i] == 0)
                {
                    App.missCount++;  
                    HoleUnhighlight(Holes[i]);
                    ResetCounter(i);
                }

                Counters[i]--;
            }

            if (currentNoOfMoles < maxNoOfMoles) HoleSelect();

            if (timeNow == maxGameTime + 1)
            {
                Timer.Stop();
                GameFinished();
            }
        }

        private void GameFinished()
        {
            this.NavigationService.Navigate(new Uri("GameEndPage.xaml", UriKind.Relative));
        }
 
        private void LevelText_TextChanged(object sender, TextChangedEventArgs e)
        {
            levelText.Text = App.gameLevel.ToString();
        }
        
        private void ResetCounter(int holeIndex)
        {
            Counters[holeIndex] = (int)maxAppearTime;
        }

        private void OnButtonMouseClick(object sender, RoutedEventArgs e)
        {
            Button hole = sender as Button;
            int holeIndex = int.Parse(hole.Content.ToString()) - 1;

            App.hitCount++;
            ResetCounter(holeIndex);
            HoleUnhighlight(hole);
        }
    }
}