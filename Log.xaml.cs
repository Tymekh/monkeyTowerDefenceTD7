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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace monkeyTowerDefenceTD7
{
    /// <summary>
    /// Logika interakcji dla klasy Log.xaml
    /// </summary>
    public partial class Log : Window
    {
        public static string Tekst;
        DispatcherTimer Refresh = new DispatcherTimer();
        public Log()
        {
            Refresh = new DispatcherTimer();
            Refresh.Interval = TimeSpan.FromSeconds((double)1 / 5);
            Refresh.Tick += Refresh_Tick;
            Refresh.Start();
            InitializeComponent();
        }

        private void Refresh_Tick(object? sender, EventArgs e)
        {
            TextBox.Text = Tekst;
            Scroll.ScrollToBottom();
        }
    }
}
