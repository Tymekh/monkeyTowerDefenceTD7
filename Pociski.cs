using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using Rectangle = System.Windows.Shapes.Rectangle;
using Point = System.Windows.Point;

namespace monkeyTowerDefenceTD7
{
    internal class Pociski
    {
        private static DispatcherTimer BulletTimer = new DispatcherTimer();
        private static List<Bullets> BulletList = new List<Bullets>();
        private static Canvas canvas;
        public class Bullets
        {
            public Rectangle Bullet {  get; set; }
            public Malpa Malpa { get; set; }
            public double Lifetime {  get; set; }
        }
        private static void BulletTimer_Tick(object? sender, EventArgs e)
        { 
            for(int i = 0; i < BulletList.Count; i++)
            {
                Rectangle Bullet = BulletList[i].Bullet;
                Malpa Target = BulletList[i].Malpa;
                BulletList[i].Lifetime += (double)1 / 60;


                double Angle = CalculateAngle(Bullet, Target);

                double BulletSpeed = 3;

                // change in movement
                double xMovement = Math.Cos(Angle) * BulletSpeed;
                double yMovement = Math.Sin(Angle) * BulletSpeed;

                Canvas.SetLeft(Bullet, Canvas.GetLeft(Bullet) + xMovement);
                Canvas.SetTop(Bullet, Canvas.GetTop(Bullet) + yMovement);

                
                if (BulletList[i].Lifetime > 4) // Check if bullet is older than specified time
                {
                    canvas.Children.Remove(Bullet);
                    BulletList.RemoveAt(i);
                }
            }
        }

        public static void Shot(Malpa target, Point StartPoint)
        {
            Rectangle bullet = new Rectangle
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Aquamarine
            };
            //BulletList.Add(bullet);
            //TargetList.Add(target);

            BulletList.Add(new Bullets { 
                Bullet = bullet,
                Malpa = target,
                Lifetime = 0,
            });

            //ArraysList.Add(();
            Canvas.SetLeft(bullet, StartPoint.X - bullet.Width / 2);
            Canvas.SetTop(bullet, StartPoint.Y - bullet.Height / 2);
            MainWindow.MyGame.Children.Add(bullet);

            if(BulletTimer.IsEnabled == false) // Check if timer is running and starts it once
            {
                canvas = MainWindow.MyGame;
                BulletTimer = new DispatcherTimer();
                BulletTimer.Interval = TimeSpan.FromSeconds((double)1 / 60);
                BulletTimer.Tick += BulletTimer_Tick;
                BulletTimer.Start();
            }
        }

        private static double CalculateAngle(FrameworkElement point1, FrameworkElement point2)
        {
            double x1 = Canvas.GetLeft(point1) + point1.ActualWidth / 2;
            double y1 = Canvas.GetTop(point1) + point1.ActualHeight / 2;
            double x2 = Canvas.GetLeft(point2) + point2.ActualWidth / 2;
            double y2 = Canvas.GetTop(point2) + point2.ActualHeight / 2;

            double angle = Math.Atan2((y2 - y1), (x2 - x1)); //calculate angle in radians
            return angle;
        }
    }
}
