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
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace monkeyTowerDefenceTD7
{
    internal class Pociski
    {
        private DispatcherTimer BulletTimer = new DispatcherTimer();
        Rectangle Bullet;
        Malpa Target;
        //double Angle;

        public Pociski() {
            BulletTimerStart();
        }

        private void BulletTimerStart()
        {
            BulletTimer = new DispatcherTimer();
            BulletTimer.Interval = TimeSpan.FromSeconds((double)1/60);
            BulletTimer.Tick += BulletTimer_Tick;
            BulletTimer.Start();
        }

        private void BulletTimer_Tick(object? sender, EventArgs e)
        {
            double x1 = Canvas.GetLeft(Bullet) + Bullet.ActualWidth / 2;
            double y1 = Canvas.GetTop(Bullet) + Bullet.ActualHeight / 2;
            double x2 = Canvas.GetLeft(Target) + Target.ActualWidth / 2;
            double y2 = Canvas.GetTop(Target) + Target.ActualHeight / 2;
            Point MalpaPosition = new Point(x1, y1);
            Point BronPosition = new Point(x2, y2);
            double Angle = CalculateAngle(MalpaPosition, BronPosition);

            double BulletSpeed = 3;

            // change in movement
            double xMovement = Math.Cos(Angle) * BulletSpeed;
            double yMovement = Math.Sin(Angle) * BulletSpeed;

            // setting position
            Canvas.SetLeft(Bullet, Canvas.GetLeft(Bullet) + xMovement);
            Canvas.SetTop(Bullet, Canvas.GetTop(Bullet) + yMovement);
        }

        public void Shot(Malpa target, Point StartPoint)
        {
            //Angle = AngleBullet;
            Target = target;

            Rectangle bullet = new Rectangle
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Aquamarine
            };
            Bullet = bullet;
            Canvas.SetLeft(Bullet, StartPoint.X - Bullet.Width / 2);
            Canvas.SetTop(Bullet, StartPoint.Y - Bullet.Height / 2);
            MainWindow.MyGame.Children.Add(Bullet);
        }

        public double CalculateAngle(Point Point1, Point Point2)
        {
            double angle = Math.Atan2((Point2.Y - Point1.Y), (Point2.X - Point1.X)); //calculate angle in radians
            return angle;
        }
    }
}
