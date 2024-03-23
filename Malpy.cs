using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace monkeyTowerDefenceTD7
{
    internal class Malpy
    {
        public static List<Rectangle> MalpaList = new List<Rectangle>();
        public void CreateMalpa(double x, double y)
        {
            ImageBrush image = new ImageBrush {
                ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Zolty/ZoltyBalon.png"))
            };
            Rectangle Malpa = new Rectangle
            {
                Width = 100,
                Height = 100,
                Fill = image
            };
            Canvas.SetLeft(Malpa, x - Malpa.Width / 2);
            Canvas.SetTop(Malpa, y - Malpa.Width / 2);
            MalpaList.Add(Malpa);
            MainWindow.MyGame.Children.Add(Malpa);
        }

    }
}
