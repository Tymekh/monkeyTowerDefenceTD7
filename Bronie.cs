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
        private void Luk(double x, double y)
        {
            Rectangle Tymczasowy = new Rectangle
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Black
            };
            Canvas.SetLeft(Tymczasowy, x);
            Canvas.SetTop(Tymczasowy, y);
            MainWindow.MyGame.Children.Add(Tymczasowy);
        }
        private void Wlocznia(double x, double y)
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
