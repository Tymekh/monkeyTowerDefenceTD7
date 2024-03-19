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
    internal class Balony
    {
        public void CreateBalon(int id, double x, double y)
        {
            Rectangle Balon = new Rectangle
            {
                Width = 50,
                Height = 50,
                Fill = Brushes.Red
            };
            Canvas.SetLeft(Balon, x - Balon.Width/2);
            Canvas.SetTop(Balon, y - Balon.Width/2);
            MainWindow.MyGame.Children.Add(Balon);
            Bronie bron = new Bronie();
            bron.StworzBron(id, x, y);
        }
        
    }
}
