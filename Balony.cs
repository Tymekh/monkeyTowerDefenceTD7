using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace monkeyTowerDefenceTD7
{
    internal class Balony
    {
        public static void CreateBalon(int id, Point BallonPosition)
        {
            ImageBrush image = new ImageBrush { };
            switch (id)
            {
                case 0:
                    {
                        image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/Czerwony/CzerwonyBalon.png"));
                        break;
                    }
                case 1:
                    {
                        image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/Niebieski/NiebieskiBalon.png"));
                        break;
                    }
                case 2:
                    {
                        image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/Brazowy/BrazowyBalon.png"));
                        break;
                    }
                case 3:
                    {
                        image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/Czarny/CzarnyBalon.png"));
                        break;
                    }
                case 4:
                    {
                        image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/Zolty/ZoltyBalon.png"));
                        break;
                    }
                case 5:
                    {
                        image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Balony/Zielony/ZielonyBalon.png"));
                        break;
                    }
                default: { break; }

            }
            Rectangle Balon = new Rectangle
            {
                Width = 100,
                Height = 100,
                Fill = image
            };
            Canvas.SetLeft(Balon, BallonPosition.X - Balon.Width/2);
            Canvas.SetTop(Balon, BallonPosition.Y - Balon.Width/2);
            MainWindow.MyGame.Children.Add(Balon);
            Bronie bron = new Bronie();
            bron.StworzBron(Balon, id, BallonPosition);
        }
    }
}
