using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace monkeyTowerDefenceTD7
{
    internal class Bronie
    {
        private DispatcherTimer WeaponTimer = new DispatcherTimer();
        private DispatcherTimer RotateTimer = new DispatcherTimer();
        private DispatcherTimer RechargeTimer = new DispatcherTimer();
        System.Windows.Shapes.Rectangle bron;
        Point BronPosition;
        bool Recharged = true;
        int id;
        int Range;
        double LifetimeLimit = 10; // domyślna długość pocisku
        double Size = 10; // domyślny rozmiar
        int StartingDistance = 0; // jak oddalony powinien byc pocisk od punktu startowego
        int Dmg = 0; // domyślny dmg
        bool isExplosive = false;
        bool isZolty = false;
        ImageBrush Bulletimage = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/invisible.png"))}; // domyślny wygląd pocisku
        Rectangle Ballon;


        public Bronie(){
            RotateTimerStart();
            RechargeTimerStart();
        }

        private void RotateTimerStart()
        {
            RotateTimer = new DispatcherTimer();
            RotateTimer.Interval = TimeSpan.FromSeconds((double)1 / 60);
            RotateTimer.Tick += RotateTimer_Tick;
            RotateTimer.Start();
        }

        private void RechargeTimerStart()
        {
            RechargeTimer = new DispatcherTimer();
            RechargeTimer.Tick += RechargeTimer_Tick;
        }

        private void RechargeTimer_Tick(object? sender, EventArgs e)
        {
            Recharged = true;
            RechargeTimer.Stop();
        }

        private void RotateTimer_Tick(object? sender, EventArgs e)
        {
            Malpa Target = null; // resetujemy wartośći dla każdej iteracji
            Nullable<double> LowestDistance = null;
            if (bron != null && MainWindow.MyGame.Children.OfType<Malpa> != null)
            {
                foreach (Malpa malpa in MainWindow.MyGame.Children.OfType<Malpa>()) // sprawdzamy dystans dla każdej małpy na ekranie
                {
                    double Distance = CalculateDistance(bron, malpa);
                    if ((Distance < LowestDistance) || (LowestDistance == null))
                    {
                        Target = malpa; // zapisz małpe z najmiejszym dystansem do balona
                        LowestDistance = Distance; // zapisz wartość najmiejszy dystans
                    }
                }
                if((LowestDistance <= Range) && Target != null) // Sprawdz czy cel jest w zasięgu małpki
                {
                    double Angle = CalculateAngle(Target, bron);
                    RotateTransform rotation = new RotateTransform(Angle * 180 / Math.PI);
                    bron.RenderTransformOrigin = new Point(0.5, 0.5);
                    bron.RenderTransform = rotation; // obrót broni w strone celu
                    if (Recharged)
                    {
                        Pociski.Shot(Target, BronPosition, LifetimeLimit, Size, Dmg, isExplosive, isZolty, Bulletimage, StartingDistance); // Strzał
                        if(isExplosive)
                        {
                            MainWindow.MyGame.Children.Remove(Ballon);
                            MainWindow.MyGame.Children.Remove(bron);
                        }
                        if (RechargeTimer.IsEnabled == false) // Zobacz czy timer jest włączony
                        {
                            Recharged = false;
                            RechargeTimer.Start();
                        }
                    }
                }
            }
        }

        public void StworzBron(Rectangle Balon, int idBroni, Point BalonPosition) // tworzy bron
        {
            Ballon = Balon;
            id = idBroni;
            ImageBrush image = new ImageBrush { };
            switch (id) // po id switch wie jaki bron stworzyć
            {
                case 0: // Czerwony
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/Czerwony/luke.png"));
                    RechargeTimer.Interval = TimeSpan.FromSeconds((double)2.5);
                    Range = 500;
                    LifetimeLimit = 5;
                    Size = 50;
                    Dmg = 20;
                    Bulletimage.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Bullets/pociskCzerwony.png"));
                    break;
                case 1: // Niebieski
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/Niebieski/dzida.png"));
                    RechargeTimer.Interval = TimeSpan.FromSeconds(1);
                    Range = 150;
                    LifetimeLimit = 0.01;
                    Size = 100;
                    StartingDistance = 50;
                    Dmg = 15;
                    break;
                case 2: // Brązowy (wybuchająca)
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/invisible.png"));
                    RechargeTimer.Interval = TimeSpan.FromSeconds(36000);
                    Range = 100;
                    LifetimeLimit = (double)0.5;
                    Size = 200;
                    Dmg = 50;
                    isExplosive = true;
                    Bulletimage.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Bullets/megumin.png"));
                    break;
                case 3: // Czarny (nic nie robi)
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/invisible.png"));
                    RechargeTimer.Interval = TimeSpan.FromSeconds(36000);
                    Range = 0;
                    LifetimeLimit = 5;
                    Size = 10;
                    Dmg = 0;
                    break;
                case 4: // Źółty
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/Zolty/petarda.png"));
                    RechargeTimer.Interval = TimeSpan.FromSeconds(3);
                    Range = 200;
                    LifetimeLimit = 5;
                    Size = 30;
                    Dmg = 0;
                    isZolty = true;
                    Bulletimage.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Bullets/pociskZolty.png"));
                    break;
                case 5: // Zielony
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/Zielony/dmuh.png"));
                    RechargeTimer.Interval = TimeSpan.FromSeconds((double)0.5);
                    Range = 750;
                    LifetimeLimit = 5;
                    Size = 15;
                    Dmg = 4;
                    Bulletimage.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Bullets/pociskZielony.png"));
                    break;
                default: // Default (testowanie)
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/BronPlaceholder.png"));
                    RechargeTimer.Interval = TimeSpan.FromSeconds(1);
                    Range = 5000;
                    LifetimeLimit = 100;
                    Size = 10;
                    Dmg = 100000;
                    break;
            }
            Rectangle Bron = new Rectangle
            {
                Width = 100,
                Height = 100,
                Fill = image
            };
            double BronX = BalonPosition.X - Bron.Width / 2;
            double BronY = BalonPosition.Y - Bron.Height / 2;
            BronPosition = new Point(BalonPosition.X, BalonPosition.Y);

            Canvas.SetLeft(Bron, BronX);
            Canvas.SetTop(Bron, BronY);
            MainWindow.MyGame.Children.Add(Bron); // dodaje bron do canvasa
            bron = Bron;
        }

        public double CalculateAngle(FrameworkElement point1, FrameworkElement point2)
        {
            double x1 = Canvas.GetLeft(point1) + point1.ActualWidth / 2;
            double y1 = Canvas.GetTop(point1) + point1.ActualHeight / 2;
            double x2 = Canvas.GetLeft(point2) + point2.ActualWidth / 2;
            double y2 = Canvas.GetTop(point2) + point2.ActualHeight / 2;

            double angle = Math.Atan2((y2 - y1), (x2 - x1)); // oblicza kąt pomiędzy 2 elementami na canvasie
            return angle;
        }
        public double CalculateDistance(FrameworkElement point1, FrameworkElement point2)
        {
            double x1 = Canvas.GetLeft(point1) + point1.ActualWidth / 2;
            double y1 = Canvas.GetTop(point1) + point1.ActualHeight / 2;
            double x2 = Canvas.GetLeft(point2) + point2.ActualWidth / 2;
            double y2 = Canvas.GetTop(point2) + point2.ActualHeight / 2;

            double Distance = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)); // oblicza dystans pomiędzy 2 elementami
            return Distance; 
        }
    }
}
