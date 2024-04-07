using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
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
        Rectangle Target;
        //double Angle;
        int id;

        public Pociski() {
            BulletTimerStart();
        }

        private void BulletTimerStart()
        {
            BulletTimer = new DispatcherTimer();
            BulletTimer.Interval = TimeSpan.FromMilliseconds(1);
            BulletTimer.Tick += BulletTimer_Tick;
            BulletTimer.Start();
        }

        private void BulletTimer_Tick(object? sender, EventArgs e)
        {
            double x1 = Canvas.GetLeft(Bullet) + Bullet.ActualWidth / 2;
            double y1 = Canvas.GetTop(Bullet) + Bullet.ActualHeight / 2;
            double x2 = Canvas.GetLeft(Target) + Target.ActualWidth / 2;
            double y2 = Canvas.GetTop(Target) + Target.ActualHeight / 2;
            double Angle = CalculateAngle(x1, y1, x2, y2);

            int BulletSpeed = 3;

            // change in movement
            double xMovement = Math.Cos(Angle) * BulletSpeed;
            double yMovement = Math.Sin(Angle) * BulletSpeed;

            // setting position
            Canvas.SetLeft(Bullet, Canvas.GetLeft(Bullet) + xMovement);
            Canvas.SetTop(Bullet, Canvas.GetTop(Bullet) + yMovement);
        }

        public void Shot(int idBullet, double AngleBullet, Rectangle TargetBullet, double x, double y)
        {
            //Angle = AngleBullet;
            Target = TargetBullet;
            id = idBullet;

            Rectangle bullet = new Rectangle
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Aquamarine
            };
            Bullet = bullet;
            Canvas.SetLeft(Bullet, x - Bullet.Width / 2);
            Canvas.SetTop(Bullet, y - Bullet.Height / 2);
            MainWindow.MyGame.Children.Add(Bullet);
        }

        public double CalculateAngle(double x1, double y1, double x2, double y2)
        {
            double angle = Math.Atan2((y2 - y1), (x2 - x1)); //calculate angle in radians
            return angle;
        }
    }
}
