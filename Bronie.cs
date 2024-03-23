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

namespace monkeyTowerDefenceTD7
{
    internal class Bronie
    {
        private DispatcherTimer WeaponTimer = new DispatcherTimer();
        
        public void StworzBron(int id, double x, double y)
        {
            switch (id)
            {
                case 0:
                    Luk(x,y);
                    return;
                case 1:
                    Wlocznia(x,y);
                    return;
                case 2:
                    // Brazowy balon
                    return;
                case 3:
                    // Czarny balon
                    return;
                case 4:
                    Dynamit(x,y);
                    return;
                case 5:
                    Rurka(x,y);
                    return;
                default: 
                    return;
            }
        }
        private void Luk(double x, double y)
        {
            ImageBrush image = new ImageBrush { };
            image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Czerwony/luke.png"));
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
        private void Wlocznia(double x, double y)
        {
            ImageBrush image = new ImageBrush { };
            image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Niebieski/dzida.png"));
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
        private void Dynamit(double x, double y)
        {
            ImageBrush image = new ImageBrush { };
            image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Zolty/petarda.png"));
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
        private void Rurka(double x, double y)
        {
            ImageBrush image = new ImageBrush { };
            image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Zielony/dmuh.png"));
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
