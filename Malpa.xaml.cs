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
using System.Windows.Threading;

namespace monkeyTowerDefenceTD7
{
    /// <summary>
    /// Logika interakcji dla klasy Malpa.xaml
    /// </summary>
    public partial class Malpa : UserControl
    {
        public DispatcherTimer TimerRuchu = new();
        public int Predkosc;
        public int Pozycja;
        public Malpa()
        {
            InitializeComponent();

            TimerRuchu.Tick += Malpy_Tick;
        }
        void Malpy_Tick(object? sender, EventArgs e)
        {
            try
            {
                if (!(MainWindow.Punkty[Pozycja].X == Canvas.GetLeft(this) + Width / 2 &&
                    MainWindow.Punkty[Pozycja].Y == Canvas.GetTop(this) + Width / 2))
                {
                    if (MainWindow.Punkty[Pozycja].X < Canvas.GetLeft(this) + Width / 2)
                    {
                        Canvas.SetLeft(this, Canvas.GetLeft(this) - 5);
                    }
                    else if (MainWindow.Punkty[Pozycja].X > Canvas.GetLeft(this) + Width / 2)
                    {
                        Canvas.SetLeft(this, Canvas.GetLeft(this) + 5);
                    }
                    else if (MainWindow.Punkty[Pozycja].Y < Canvas.GetTop(this) + Height / 2)
                    {
                        Canvas.SetTop(this, Canvas.GetTop(this) - 5);
                    }
                    else if (MainWindow.Punkty[Pozycja].Y > Canvas.GetTop(this) + Height / 2)
                    {
                        Canvas.SetTop(this, Canvas.GetTop(this) + 5);
                    }
                    else MessageBox.Show("jesli to sie pokazalo to cos sie mega zepsulo xd");
                }
                else
                {
                    Pozycja++;
                }
            }
            catch (Exception)
            {
                TimerRuchu.Stop();
                MainWindow.Zycie -= 5;
                MessageBox.Show(MainWindow.Zycie.ToString());
                MainWindow.MyGame.Children.Remove(this);
            }
        }
    }
}
