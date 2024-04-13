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
        double LifetimeLimit = 10; // Default Lifetime (in seconds)
        double Size = 10; // Default size
        int StartingDistance = 0; // How far should bullet spawn


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
            Malpa Target = null;
            Nullable<double> LowestDistance = null;
            if (bron != null && MainWindow.MyGame.Children.OfType<Malpa> != null)
            {
                foreach (Malpa malpa in MainWindow.MyGame.Children.OfType<Malpa>())
                {
                    double Distance = CalculateDistance(bron, malpa);
                    if ((Distance < LowestDistance) || (LowestDistance == null))
                    {
                        Target = malpa; // Save monkey with lowest distance to balloon
                        LowestDistance = Distance; // Save value of lowest distance
                    }
                }
                if((LowestDistance <= Range) && Target != null) // Check if Target is in range
                {
                    double Angle = CalculateAngle(Target, bron);
                    RotateTransform rotation = new RotateTransform(Angle * 180 / Math.PI);
                    bron.RenderTransformOrigin = new Point(0.5, 0.5);
                    bron.RenderTransform = rotation;
                    if (Recharged)
                    {
                        Pociski.Shot(Target, BronPosition, LifetimeLimit, Size, StartingDistance);
                        if (RechargeTimer.IsEnabled == false) // Check if timer is disabled to avoid starting it multiple times
                        {
                            Recharged = false;
                            RechargeTimer.Start();
                        }
                    }
                }
            }
        }

        public void StworzBron(int idBroni, Point BalonPosition)
        {
            id = idBroni;
            ImageBrush image = new ImageBrush { };
            switch (id) // id jest domyślnie a do testowania wstawić np. 10
            {
                case 0: // Czerwony
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/Czerwony/luke.png"));
                    RechargeTimer.Interval = TimeSpan.FromSeconds(1);
                    Range = 500;
                    LifetimeLimit = 5;
                    Size = 10;
                    break;
                case 1: // Niebieski
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/Niebieski/dzida.png"));
                    RechargeTimer.Interval = TimeSpan.FromSeconds(1);
                    Range = 100;
                    LifetimeLimit = 0.01;
                    Size = 100;
                    StartingDistance = 50;
                    break;
                case 2: // Brązowy
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/invisible.png"));
                    RechargeTimer.Interval = TimeSpan.FromSeconds(1);
                    Range = 500;
                    LifetimeLimit = 5;
                    Size = 10;
                    break;
                case 3: // Czarny
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/invisible.png"));
                    RechargeTimer.Interval = TimeSpan.FromSeconds(1);
                    Range = 500;
                    LifetimeLimit = 5;
                    Size = 10;
                    break;
                case 4: // Źółty
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/Zolty/petarda.png"));
                    RechargeTimer.Interval = TimeSpan.FromSeconds(1);
                    Range = 500;
                    LifetimeLimit = 5;
                    Size = 10;
                    break;
                case 5: // Zielony
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/Zielony/dmuh.png"));
                    RechargeTimer.Interval = TimeSpan.FromSeconds(1);
                    Range = 500;
                    LifetimeLimit = 5;
                    Size = 10;
                    break;
                default: // Default (testowanie)
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/BronPlaceholder.png"));
                    RechargeTimer.Interval = TimeSpan.FromSeconds(4);
                    Range = 5000;
                    LifetimeLimit = 10;
                    Size = 10;
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
            MainWindow.MyGame.Children.Add(Bron);
            bron = Bron;
        }

        public double CalculateAngle(FrameworkElement point1, FrameworkElement point2)
        {
            double x1 = Canvas.GetLeft(point1) + point1.ActualWidth / 2;
            double y1 = Canvas.GetTop(point1) + point1.ActualHeight / 2;
            double x2 = Canvas.GetLeft(point2) + point2.ActualWidth / 2;
            double y2 = Canvas.GetTop(point2) + point2.ActualHeight / 2;

            double angle = Math.Atan2((y2 - y1), (x2 - x1)); //calculate angle in radians
            return angle;
        }
        public double CalculateDistance(FrameworkElement point1, FrameworkElement point2)
        {
            double x1 = Canvas.GetLeft(point1) + point1.ActualWidth / 2;
            double y1 = Canvas.GetTop(point1) + point1.ActualHeight / 2;
            double x2 = Canvas.GetLeft(point2) + point2.ActualWidth / 2;
            double y2 = Canvas.GetTop(point2) + point2.ActualHeight / 2;

            double Distance = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)); //calculating distance betwen 2 points
            return Distance; 
        }
    }
}
