using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace _3080
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static int level;
        static int hitNo;
        static int missNo;

        public static int gameLevel
        {
            get { return level; }
            set { level = value; }
        }

        public static int hitCount
        {
            get { return hitNo; }
            set { hitNo = value; }
        }

        public static int missCount
        {
            get { return missNo; }
            set { missNo = value; }
        }

    }
}
