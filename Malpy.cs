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
        public static List<int> PozycjeMalp = new();
        public void CreateMalpa(double x, double y)
        {
            ImageBrush image = new ImageBrush {
                ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Maupy/Maupa.png"))
            };
            Rectangle Malpa = new Rectangle
            {
                Width = 80,
                Height = 80,
                Fill = image
            };
            Canvas.SetLeft(Malpa, x - Malpa.Width / 2);
            Canvas.SetTop(Malpa, y - Malpa.Width / 2);
            MalpaList.Add(Malpa);
            PozycjeMalp.Add(0);
            MainWindow.MyGame.Children.Add(Malpa);
        }

        public static void MalpaNaSciezce(int id, double x, double y)
        {
            ImageBrush image = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(@"pack://application:,,/img/Maupy/Maupa.png"))
            };
            Canvas.SetTop(MalpaList[id], y - MalpaList[id].Height / 2);
            Canvas.SetLeft(MalpaList[id], x - MalpaList[id].Width / 2);
        }
    }
}
