﻿using System;
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
        Rectangle bron;
        public static double Distance;
        double Index;
        double Angle;
        double Dst1 = 1000;
        bool Recharged = false;
        int id;

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
            if (bron != null)
            {
                for (int i = 0; i < Malpy.MalpaList.Count; i++)
                {
                    double x1 = Canvas.GetLeft(Malpy.MalpaList[i]) + Malpy.MalpaList[i].ActualWidth / 2;
                    double y1 = Canvas.GetTop(Malpy.MalpaList[i]) + Malpy.MalpaList[i].ActualHeight / 2;
                    double x2 = Canvas.GetLeft(bron) + bron.ActualWidth / 2;
                    double y2 = Canvas.GetTop(bron) + bron.ActualHeight / 2;
                    double Dst = CalculateDistance(x1, y1, x2, y2);
                    if (Dst < Dst1)
                    {
                        Dst1 = Dst;
                        Index = i;
                        Angle = CalculateAngle(x1, y1, x2, y2);
                    }
                }
                RotateTransform rotation = new RotateTransform(Angle * 180 / Math.PI);
                bron.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
                bron.RenderTransform = rotation;
            }
            if(Recharged)
            {
                Pociski.Shot(id, Angle);
            }
        }

        public void StworzBron(int id, double x, double y)
        {
            ImageBrush image = new ImageBrush { };
            switch (id) // id jest domyślnie a do testowania wstawić np. 10
            {
                case 0:
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/Czerwony/luke.png"));
                    break;
                case 1:
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/Niebieski/dzida.png"));
                    break;
                case 2:
                    // Brazowy balon
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/invisible.png"));
                    break;
                case 3:
                    // Czarny balon
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/invisible.png"));
                    break;
                case 4:
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/Zolty/petarda.png"));
                    break;
                case 5:
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/Zielony/dmuh.png"));
                    break;
                default:
                    image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/BronPlaceholder.png"));
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
