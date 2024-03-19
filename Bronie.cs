using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace monkeyTowerDefenceTD7
{
    internal class Bronie
    {
        public static void StworzBron(int id)
        {
            switch (id)
            {
                case 0:
                    Luk();
                    return;
                case 1:
                    Wlocznia();
                    return;
                case 2:
                    return;
                case 3:
                    return;
                case 4:
                    return;
                case 5:
                    return;
                case 6:
                    return;
                case 7:
                    return;
                default: 
                    return;
            }
        }
        private static void Luk()
        {
            Rectangle Tymczasowy = new Rectangle
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Black
            };
            Canvas.SetLeft(Tymczasowy, 100);
            Canvas.SetTop(Tymczasowy, 100);
            MainWindow.MyGame.Children.Add(Tymczasowy);
        }
        private static void Wlocznia()
        {
            Rectangle Tymczasowy = new Rectangle
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Brown
            };
            Canvas.SetLeft(Tymczasowy, 200);
            Canvas.SetTop(Tymczasowy, 100);
            MainWindow.MyGame.Children.Add(Tymczasowy);
        }
    }
}
