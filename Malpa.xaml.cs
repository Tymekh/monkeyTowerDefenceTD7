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
        public int Pozycja = 0;
        public int Predkosc = 5;
        public Malpa()
        {
            InitializeComponent();
        }
        public void Malpy_Tick()
        {
            if (Pozycja < MainWindow.Punkty.Count)
            {
                if (!(MainWindow.Punkty[Pozycja].X == Canvas.GetLeft(this) + ActualWidth / 2 &&
                    MainWindow.Punkty[Pozycja].Y == Canvas.GetTop(this) + ActualWidth / 2))
                {
                    // na 100% mój kod, nie ukradnięty z Pociski.cs

                    double x1 = Canvas.GetLeft(this) + ActualWidth / 2;
                    double y1 = Canvas.GetTop(this) + ActualHeight / 2;
                    double x2 = MainWindow.Punkty[Pozycja].X;
                    double y2 = MainWindow.Punkty[Pozycja].Y;
                    double Angle = Math.Atan2(y2 - y1, x2 - x1);

                    // change in movement
                    double xMovement = Math.Cos(Angle) * Predkosc;
                    double yMovement = Math.Sin(Angle) * Predkosc;

                    // setting position
                    Canvas.SetLeft(this, Canvas.GetLeft(this) + xMovement);
                    Canvas.SetTop(this, Canvas.GetTop(this) + yMovement);
                }
                else
                {
                    Pozycja++;
                }
            }
            else
            {
                MainWindow.Zycie -= 5;
                MessageBox.Show(MainWindow.Zycie.ToString());
                MainWindow.MyGame.Children.Remove(this);
            }
        }
    }
}
