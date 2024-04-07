using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Controls;
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
        Rectangle bron;
        Rectangle Target;
        double Angle;
        double Dst1 = 1000;
        bool Recharged = true;
        int Index;
        int id;
        int Range;

        public Bronie(){
            RotateTimerStart();
            RechargeTimerStart();
        }

        private void RotateTimerStart()
        {
            RotateTimer = new DispatcherTimer();
            RotateTimer.Interval = TimeSpan.FromMilliseconds(1);
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
            if (bron != null)
            {
                for (int i = 0; i < Malpy.MalpaList.Count; i++)
                {
                    double x1 = Canvas.GetLeft(Malpy.MalpaList[i]) + Malpy.MalpaList[i].ActualWidth / 2;
                    double y1 = Canvas.GetTop(Malpy.MalpaList[i]) + Malpy.MalpaList[i].ActualHeight / 2;
                    double x2 = Canvas.GetLeft(bron) + bron.ActualWidth / 2;
                    double y2 = Canvas.GetTop(bron) + bron.ActualHeight / 2;
                    double Dst = CalculateDistance(x1, y1, x2, y2);
                    if ((Dst < Dst1) && (Dst < Range))
                    {
                        Dst1 = Dst;
                        Index = i;
                        Target = Malpy.MalpaList[i];
                        Angle = CalculateAngle(x1, y1, x2, y2);
                    }
                }
                RotateTransform rotation = new RotateTransform(Angle * 180 / Math.PI);
                bron.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
                bron.RenderTransform = rotation;
            }
            if(Recharged && Target != null)
            {
                double x = Canvas.GetLeft(bron) + bron.ActualWidth / 2;
                double y = Canvas.GetTop(bron) + bron.ActualHeight / 2;
                Pociski pocisk = new Pociski();
                pocisk.Shot(id, Angle, Target, x, y);
                if(!RechargeTimer.IsEnabled)
                {
                    Recharged = false;
                    RechargeTimer.Start();
                }
            }
        }

        public void StworzBron(int idBroni, double x, double y)
        {
            id = idBroni;
            ImageBrush image = new ImageBrush { };
            switch (10) // id jest domyślnie a do testowania wstawić np. 10
            {
                case 0:
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/Czerwony/luke.png"));
                    RechargeTimer.Interval = TimeSpan.FromSeconds(1);
                    Range = 500;
                    break;
                case 1:
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/Niebieski/dzida.png"));
                    RechargeTimer.Interval = TimeSpan.FromSeconds(1);
                    Range = 500;
                    break;
                case 2:
                    // Brazowy balon
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/invisible.png"));
                    RechargeTimer.Interval = TimeSpan.FromSeconds(1);
                    Range = 500;
                    break;
                case 3:
                    // Czarny balon
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/invisible.png"));
                    RechargeTimer.Interval = TimeSpan.FromSeconds(1);
                    Range = 500;
                    break;
                case 4:
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/Zolty/petarda.png"));
                    RechargeTimer.Interval = TimeSpan.FromSeconds(1);
                    Range = 500;
                    break;
                case 5:
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/Zielony/dmuh.png"));
                    RechargeTimer.Interval = TimeSpan.FromSeconds(1);
                    Range = 500;
                    break;
                default:
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/BronPlaceholder.png"));
                    RechargeTimer.Interval = TimeSpan.FromMilliseconds(1);
                    Range = 5000;
                    break;
            }
            Rectangle Bron = new Rectangle
            {
                Width = 100,
                Height = 100,
                Fill = image
            };
            Canvas.SetLeft(Bron, x - Bron.Width / 2);
            Canvas.SetTop(Bron, y - Bron.Height / 2);
            MainWindow.MyGame.Children.Add(Bron);
            bron = Bron;
        }

        public double CalculateAngle(double x1, double y1, double x2, double y2)
        {
            double angle = Math.Atan2((y2 - y1), (x2 - x1)); //calculate angle in radians
            return angle;
        }
        public double CalculateDistance(double x1, double y1, double x2, double y2)
        {
            double Dst = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)); //calculating distance betwen 2 points
            return Dst; 
        }
    }
}
