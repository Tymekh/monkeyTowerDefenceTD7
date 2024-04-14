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
using static monkeyTowerDefenceTD7.Pociski;
using System.Windows.Media.Imaging;

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
            public double LifetimeLimit {  get; set; }
            public int Dmg { get; set; }
            public bool isExplosive { get; set; }
            public bool isZolty { get; set; }
        }
        private static void BulletTimer_Tick(object? sender, EventArgs e)
        { 
            for(int i = 0; i < BulletList.Count; i++)
            {
                Rectangle Bullet = BulletList[i].Bullet;
                Malpa Target = BulletList[i].Malpa;
                BulletList[i].Lifetime += (double)1 / 60;
                int Damage = BulletList[i].Dmg;

                if (Target != null)
                {
                    double Angle = CalculateAngle(Bullet, Target);

                    //Log.Tekst += Angle.ToString() + "\n";

                    double BulletSpeed = 10;

                    // change in movement
                    double xMovement = Math.Cos(Angle) * BulletSpeed;
                    double yMovement = Math.Sin(Angle) * BulletSpeed;

                    Canvas.SetLeft(Bullet, Canvas.GetLeft(Bullet) + xMovement);
                    Canvas.SetTop(Bullet, Canvas.GetTop(Bullet) + yMovement);
                }

                if(CheckColision(Bullet, Target))
                {
                    if (BulletList[i].isZolty)
                    {
                        double x = Canvas.GetLeft(Bullet) + Bullet.ActualWidth / 2;
                        double y = Canvas.GetTop(Bullet) + Bullet.ActualHeight / 2;
                        ImageBrush image = new ImageBrush { 
                        ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Bullets/megumin.png"))
                        };
                        Pociski.Shot(Target, new Point(x,y), (double)0.5, 100, 5, true, false, image, 0);
                        DeleteBullet(i);
                        continue;
                    }
                    if (BulletList[i].isExplosive)
                    {
                        for (int j = 0; j < MainWindow.MyGame.Children.OfType<Malpa>().Count(); j++)
                        {
                            Malpa malpa = MainWindow.MyGame.Children.OfType<Malpa>().ElementAt(j);
                            if(CheckColision(Bullet, malpa))
                            {
                                if (malpa.CzyZyje) malpa.ZadajObrazenia(Damage);
                                Log.Tekst += "trafiono \n";
                            }
                        }
                        BulletList[i].Malpa = null;
                        continue;
                    }
                    //MessageBox.Show("Trafiono");
                    DeleteBullet(i);
                    if (Target.CzyZyje) Target.ZadajObrazenia(Damage);
                    continue;
                }

                if (BulletList[i].Lifetime > BulletList[i].LifetimeLimit) // Check if bullet is older than specified time
                {
                    DeleteBullet(i);
                }
            }
        }

        public static void Shot(Malpa Target, Point StartPoint,double LifetimeLimit, double Size,int Damage,bool isExposive, bool isZolty, ImageBrush image, int StartingDistance = 0)
        {
            Rectangle bullet = new Rectangle
            {
                Width = Size,
                Height = Size,
                Fill = image,
            };
            //BulletList.Add(bullet);
            //TargetList.Add(target);
            Panel.SetZIndex(bullet, 2);
            BulletList.Add(new Bullets
            {
                Bullet = bullet,
                Malpa = Target,
                Lifetime = 0,
                LifetimeLimit = LifetimeLimit,
                Dmg = Damage,
                isExplosive = isExposive,
                isZolty = isZolty,
            });

            Canvas.SetLeft(bullet, StartPoint.X - bullet.Width / 2);
            Canvas.SetTop(bullet, StartPoint.Y - bullet.Height / 2);

            double Angle = CalculateAngle(bullet, Target);

            double xMovement = Math.Cos(Angle) * StartingDistance;
            double yMovement = Math.Sin(Angle) * StartingDistance;

            Canvas.SetLeft(bullet, Canvas.GetLeft(bullet) + xMovement);
            Canvas.SetTop(bullet, Canvas.GetTop(bullet) + yMovement);

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

        private static void DeleteBullet(int id)
        {
            canvas.Children.Remove(BulletList[id].Bullet);
            BulletList.RemoveAt(id);
        }

        private static bool CheckColision(FrameworkElement point1,  FrameworkElement point2)
        {
            if (point1 == null || point2 == null) { return false; }

            int Smaller = 1; // Says how much smaler is the hitbox acording to the bullet sice (eg. 2 is 2 times smaller)

            //var x1 = Canvas.GetLeft(point1) + (point1.ActualWidth / (Smaller * 2));
            //var y1 = Canvas.GetTop(point1) + (point1.ActualHeight / (Smaller * 2));

            var x1 = Canvas.GetLeft(point1);
            var y1 = Canvas.GetTop(point1);

            Rect HitBox1 = new Rect(x1, y1, point1.ActualWidth / Smaller, point1.ActualHeight / Smaller);

            var x2 = Canvas.GetLeft(point2);
            var y2 = Canvas.GetTop(point2);
            Rect HitBox2 = new Rect(x2, y2, point2.ActualWidth, point2.ActualHeight);

            if (HitBox1.IntersectsWith(HitBox2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static double CalculateAngle(FrameworkElement point1, FrameworkElement point2)
        {
            if (point1 == null || point2 == null) { return 0; }

            double x1 = Canvas.GetLeft(point1) + point1.ActualWidth / 2;
            double y1 = Canvas.GetTop(point1) + point1.ActualHeight / 2;
            double x2 = Canvas.GetLeft(point2) + point2.ActualWidth / 2;
            double y2 = Canvas.GetTop(point2) + point2.ActualHeight / 2;

            double angle = Math.Atan2((y2 - y1), (x2 - x1)); //calculate angle in radians
            return angle;
        }
    }
}
