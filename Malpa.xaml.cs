using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
        private int Pozycja = 0;
        private int Predkosc = 5;
        private int Wartosc = 10;
        private int Obrazenia = 1;

        private int idMalpy = 0;

        public bool CzyZyje = true;
        public Malpa(int id)
        {
            InitializeComponent();
            idMalpy = id;
            Panel.SetZIndex(this, 2);
            UstawWlasciwosci();
        }

        private void UstawWlasciwosci()
        {
            switch (idMalpy)
            {
                case 0: // Małpa zwykła
                    Obrazek.Source = new BitmapImage(new Uri(@"pack://application:,,/img/Maupy/Maupa.png"));
                    Wartosc = 5;
                    Predkosc = 5;
                    Zycie = 10;
                    Obrazenia = 1;

                    break;
                case 1: // Małpa albinos
                    Obrazek.Source = new BitmapImage(new Uri(@"pack://application:,,/img/Maupy/Maupa_Albinos.png"));
                    Wartosc = 15;
                    Predkosc = 5;
                    Zycie = 40;
                    Obrazenia = 2;

                    break;
                case 2: // Małpa czarna
                    Obrazek.Source = new BitmapImage(new Uri(@"pack://application:,,/img/Maupy/Maupa_Czarnuch.png"));
                    Wartosc = 1;
                    Predkosc = 8;
                    Zycie = 5;
                    Obrazenia = 1;

                    break;
                case 3: // Małpa z hełmem
                    Obrazek.Source = new BitmapImage(new Uri(@"pack://application:,,/img/Maupy/Maupa_Helm.png"));
                    Wartosc = 20;
                    Predkosc = 3;
                    Zycie = 120;
                    Obrazenia = 5;

                    break;
                case 4: // Małpa matka
                    Obrazek.Source = new BitmapImage(new Uri(@"pack://application:,,/img/Maupy/Maupa_Matka.png"));
                    Wartosc = 25;
                    Predkosc = 2;
                    Zycie = 80;
                    Obrazenia = 5;

                    break;
                case 5: // Małpa w zbroi
                    Obrazek.Source = new BitmapImage(new Uri(@"pack://application:,,/img/Maupy/Maupa_Zbroja.png"));
                    Wartosc = 30;
                    Predkosc = 2;
                    Zycie = 180;
                    Obrazenia = 10;

                    break;
                case 6: // Mutanty z losowom teksturą i statystykami
                    Obrazek.Source = new BitmapImage(new Uri($@"pack://application:,,/img/Maupy/Mutanty/Maupa_Mutant0{new Random().Next(1, 9)}.png"));
                    Wartosc = new Random().Next(1, 41);   
                    Predkosc = new Random().Next(1, 9);
                    Zycie = new Random().Next(1, 151);
                    Obrazenia = new Random().Next(1, 16);

                    break;
                case 7: // Małpa dziecko
                    Obrazek.Width = 50;
                    Obrazek.Height = 50;
                    Obrazek.Source = new BitmapImage(new Uri(@"pack://application:,,/img/Maupy/Maupa_Dziecko.png"));
                    Wartosc = 5;
                    Predkosc = new Random().Next(9, 13);
                    Zycie = 1;

                    break;
                default:
                    Obrazek.Source = new BitmapImage(new Uri(@"pack://application:,,/img/Maupy/Maupa.png"));
                    Wartosc = 10;
                    Predkosc = 5;
                    Zycie = 10;

                    break;
            }
        }
        public void RuchMalpy()
        {
            if (Pozycja < MainWindow.Punkty.Count) // Sprawdza czy punkt, do którego idzie małpka istnieje.
            {
                if (!(MainWindow.Punkty[Pozycja].X == Canvas.GetLeft(this) + ActualWidth / 2 &&
                    MainWindow.Punkty[Pozycja].Y == Canvas.GetTop(this) + ActualWidth / 2)) // Sprawdza czy małpka jest dokładnie w punkcie docelowym.
                {
                    double x1 = Canvas.GetLeft(this) + ActualWidth / 2; // Jeśli nie:
                    double y1 = Canvas.GetTop(this) + ActualHeight / 2;
                    double x2 = MainWindow.Punkty[Pozycja].X;
                    double y2 = MainWindow.Punkty[Pozycja].Y;
                    double Angle = Math.Atan2(y2 - y1, x2 - x1);

                    double xMovement = Math.Cos(Angle) * Predkosc;
                    double yMovement = Math.Sin(Angle) * Predkosc;

                    double Distance = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));

                    if (Distance < Predkosc) // Sprawdza czy dystans jest mniejszy od prędkości małpki:
                    {
                        Canvas.SetLeft(this, MainWindow.Punkty[Pozycja].X - ActualWidth / 2); // Jeśli tak, przenosi małpke do punktu docelowego.
                        Canvas.SetTop(this, MainWindow.Punkty[Pozycja].Y - ActualWidth / 2);
                    }
                    else
                    {
                        Canvas.SetLeft(this, Canvas.GetLeft(this) + xMovement); // Jeśli nie, małpka idzie w jego kierunku.
                        Canvas.SetTop(this, Canvas.GetTop(this) + yMovement);
                    }
                }
                else // Jeśli małpka jest w punkcie docelowym, wybiera następny punkt ścieżki jako cel.
                {
                    Pozycja++;
                }
            }
            else // Jeśli punkty się skończyły, odejmij życie i zaktualizuj wartości.
            {
                MainWindow.Zycie -= Obrazenia;
                MainWindow.AktualizujWarotsci();
                MainWindow.MyGame.Children.Remove(this);
            }
        }

        public void ZadajObrazenia(int Obrazenia)
        {   
            if (Obrazenia < Zycie)
            {   
                Zycie -= Obrazenia;
                if (idMalpy == 5 && Zycie <= 120)
                {
                    Obrazek.Source = new BitmapImage(new Uri(@"pack://application:,,/img/Maupy/Maupa_Helm.png"));
                    Predkosc = 3;
                    idMalpy = 3;
                }
                if (idMalpy == 3 && Zycie <= 10)
                {
                    Obrazek.Source = new BitmapImage(new Uri(@"pack://application:,,/img/Maupy/Maupa.png"));
                    Predkosc = 5;
                    idMalpy = 0;
                }
                return;
            }
            MainWindow.Pieniadze += Wartosc;
            MainWindow.AktualizujWarotsci();
            ZabijSie();
        }

        public void ZabijSie()
        {
            CzyZyje = false;

            Rectangle Gore = new()
            {
                Width = 50,
                Height = 50,
                Fill = new ImageBrush()
                {
                    ImageSource = new BitmapImage(new Uri($@"pack://application:,,/img/Gore/Gore{new Random().Next(1, 17)}.png"))
                }
            };

            if (idMalpy == 4)
            {
                for (int i = new Random().Next(0, 5); i < 5; i++)
                {
                    Malpa malpa = new(7)
                    {
                        Width = 100,
                        Height = 100
                    };
                    malpa.Pozycja = Pozycja;
                    Canvas.SetTop(malpa, Canvas.GetTop(this));
                    Canvas.SetLeft(malpa, Canvas.GetLeft(this));
                    MainWindow.MyGame.Children.Add(malpa);
                }
            }

            RotateTransform rotation = new RotateTransform(new Random().Next(0, 360) * 180 / Math.PI);
            Gore.RenderTransformOrigin = new Point(0.5, 0.5);
            Gore.RenderTransform = rotation;

            Panel.SetZIndex(Gore, 1);
            Canvas.SetTop(Gore, Canvas.GetTop(this) + ActualWidth / 4 + new Random().Next(-20, 20));
            Canvas.SetLeft(Gore, Canvas.GetLeft(this) + ActualWidth / 4 + new Random().Next(-20, 20));
            MainWindow.MyGame.Children.Add(Gore);
            MainWindow.MyGame.Children.Remove(this);

        }
    }
}
