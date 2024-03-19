using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace monkeyTowerDefenceTD7
{
    internal class Balony
    {
        public static void kwadrat()
        {
        Rectangle Tymczasowy = new Rectangle
        {
            Width = 10,
            Height = 10,
            Fill = Brushes.Black
        };

        MainWindow.MyGame.Children.Add(Tymczasowy);
        }
        
    }
}
