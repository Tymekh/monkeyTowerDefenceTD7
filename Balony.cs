using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace monkeyTowerDefenceTD7
{
    internal class Balony
    {
        public void CreateBalon(int id, double x, double y)
        {
            ImageBrush image = new ImageBrush { };
            switch (id)
            {
                case 0:
                    {
                        image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Czerwony/CzerwonyBalon.png"));
                        break;
                    }
                case 1:
                    {
                        image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Niebieski/NiebieskiBalon.png"));
                        break;
                    }
                case 2:
                    {
                        image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Brazowy/BrazowyBalon.png"));
                        break;
                    }
                case 3:
                    {
                        image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Czarny/CzarnyBalon.png"));
                        break;
                    }
                case 4:
                    {
                        image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Zolty/ZoltyBalon.png"));
                        break;
                    }
                case 5:
                    {
                        image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Zielony/ZielonyBalon.png"));
                        break;
                    }
                // Mozliwy bialy balon
                //case 6:
                //    {
                //        image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Czerwony/CzerwonyBalon.png"));
                //        break;
                //    }
                default: { break; }

            }
            Rectangle Balon = new Rectangle
            {
                Width = 100,
                Height = 100,
                Fill = image
            };
            Canvas.SetLeft(Balon, x - Balon.Width/2);
            Canvas.SetTop(Balon, y - Balon.Width/2);
            MainWindow.MyGame.Children.Add(Balon);
            Bronie bron = new Bronie();
            bron.StworzBron(id, x, y);
        }
        
    }
}
