using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        Rectangle Bron;
        public static double Distance;

        public Bronie(){
            RotateTimerStart();
        }

        private void RotateTimerStart()
        {
            RotateTimer = new DispatcherTimer();
            RotateTimer.Interval = TimeSpan.FromMilliseconds(1);
            RotateTimer.Tick += RotateTimer_Tick;
            RotateTimer.Start();
        }

        private void RotateTimer_Tick(object? sender, EventArgs e)
        {
            if (Bron != null)
            {
                //double Distance = 100;
                double index;
                for (int i = 0; i < Malpy.MalpaList.Count; i++)
                {
                    double dst = Math.Sqrt(Math.Pow(Canvas.GetLeft(Bron) - Canvas.GetLeft(Malpy.MalpaList[i]), 2) + Math.Pow(Canvas.GetTop(Bron) - Canvas.GetTop(Malpy.MalpaList[i]), 2));
                    if (dst < Distance)
                    {
                        Distance = dst;
                        index = i;
                    }
                }
            }
            else
            {
                return;
            }
        }

        public void StworzBron(int id, double x, double y)
        {
            ImageBrush image = new ImageBrush { };
            switch (id)
            {
                case 0:
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/Czerwony/luke.png"));
                    break;
                case 1:
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/Niebieski/dzida.png"));
                    break;
                case 2:
                    // Brazowy balon
                    break;
                case 3:
                    // Czarny balon
                    break;
                case 4:
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/Zolty/petarda.png"));
                    break;
                case 5:
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/Zielony/dmuh.png"));
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
        }
    }
}
