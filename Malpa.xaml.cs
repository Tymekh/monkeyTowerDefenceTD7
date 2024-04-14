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
        public int Zycie = 10;
        public int Pozycja = 0;
        public int Predkosc = 5;
        public int Wartosc = 10;
        public int Obrazenia = 1;

        public Malpa(int id)
        {
            InitializeComponent();
            UstawWlasciwosci(id);
        }

        private void UstawWlasciwosci(int id)
        {
            switch (id)
            {
                case 0:
                    Obrazek.Source = new BitmapImage(new Uri(@"pack://application:,,/img/Maupy/Maupa.png"));
                    Wartosc = 10;
                    Predkosc = 5;
                    Zycie = 10;

                    break;
                case 1:
                    Obrazek.Source = new BitmapImage(new Uri(@"pack://application:,,/img/Maupy/Maupa_Albinos.png"));
                    Wartosc = 10;
                    Predkosc = 5;

                    break;
                case 2:
                    Obrazek.Source = new BitmapImage(new Uri(@"pack://application:,,/img/Maupy/Maupa_Czarnuch.png"));
                    Wartosc = 1;
                    Predkosc = 8;

                    break;
                case 3:
                    Obrazek.Source = new BitmapImage(new Uri(@"pack://application:,,/img/Maupy/Maupa_Matka.png"));
                    Wartosc = 20;
                    Predkosc = 2;

                    break;
                case 4:
                    Obrazek.Source = new BitmapImage(new Uri(@"pack://application:,,/img/Maupy/Maupa_Dziecko.png"));
                    Wartosc = 5;
                    Predkosc = 10;

                    break;
                case 5:
                    Obrazek.Source = new BitmapImage(new Uri(@"pack://application:,,/img/Maupy/Maupa_Helm.png"));
                    Wartosc = 15;
                    Predkosc = 3;

                    break;
                case 6:
                    Obrazek.Source = new BitmapImage(new Uri(@"pack://application:,,/img/Maupy/Maupa_Zbroja.png"));
                    Wartosc = 20;
                    Predkosc = 2;

                    break;
                case 7:
                    Obrazek.Source = new BitmapImage(new Uri($@"pack://application:,,/img/Maupy/Mutanty/Maupa_Mutant0{new Random().Next(1, 9)}.png"));
                    Wartosc = new Random().Next(1, 41);   
                    Predkosc = new Random().Next(1, 8);

                    break;
                default:
                    Obrazek.Source = new BitmapImage(new Uri(@"pack://application:,,/img/Maupy/Maupa.png"));
                    Wartosc = 10;
                    Predkosc = 5;

                    break;
            }
        }
        public void RuchMalpy()
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

                    double Distance = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));

                    // setting position
                    if (Distance > Predkosc)
                    {
                        Canvas.SetLeft(this, Canvas.GetLeft(this) + xMovement);
                        Canvas.SetTop(this, Canvas.GetTop(this) + yMovement);
                    }
                    else
                    {
                        Canvas.SetLeft(this, MainWindow.Punkty[Pozycja].X - ActualWidth / 2);
                        Canvas.SetTop(this, MainWindow.Punkty[Pozycja].Y - ActualWidth / 2);
                    }
                }
                else
                {
                    Pozycja++;
                }
            }
            else
            {
                MainWindow.Zycie -= Obrazenia;
                MainWindow.AktualizujWarotsci();
                //MessageBox.Show(MainWindow.Zycie.ToString());
                MainWindow.MyGame.Children.Remove(this);
            }
        }
    }
}
