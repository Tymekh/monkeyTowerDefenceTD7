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
        public static void CreateBalon(int id)
        {
        //Rectangle Tymczasowy = new Rectangle
        //{
        //    Width = 10,
        //    Height = 10,
        //    Fill = Brushes.Black
        //};
        //Canvas.SetLeft(Tymczasowy, 100);
        //Canvas.SetTop(Tymczasowy, 100);
        //MainWindow.MyGame.Children.Add(Tymczasowy);
        Bronie.StworzBron(id);
        }
        
    }
}
