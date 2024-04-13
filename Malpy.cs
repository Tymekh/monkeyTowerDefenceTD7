using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace monkeyTowerDefenceTD7
{
    internal class Malpy
    {
        public static List<Rectangle> MalpaList = new List<Rectangle>();
        public static List<int> PozycjeMalp = [];

        public static List<DispatcherTimer> ListaTimerowRuchu = [];
        public static List<float> PredkosciMalp = [];

        public static void CreateMalpa(double x, double y)
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

            PredkosciMalp.Add(new Random().Next(50, 100));
        }

        public static void StartPoruszania(int id)
        {
            Canvas.SetLeft(MalpaList[id], MainWindow.Punkty[PozycjeMalp[id]].X - MalpaList[id].Width / 2);
            Canvas.SetTop(MalpaList[id], MainWindow.Punkty[PozycjeMalp[id]].Y - MalpaList[id].Height / 2);
            ListaTimerowRuchu.Add(new DispatcherTimer());
            void Malpy_Tick(object? sender, EventArgs e)
            {
                try
                {
                    if (!(MainWindow.Punkty[PozycjeMalp[id]].X == Canvas.GetLeft(MalpaList[id]) + MalpaList[id].Width / 2 &&
                        MainWindow.Punkty[PozycjeMalp[id]].Y == Canvas.GetTop(MalpaList[id]) + MalpaList[id].Width / 2))
                    {
                        if (MainWindow.Punkty[PozycjeMalp[id]].X < Canvas.GetLeft(MalpaList[id]) + MalpaList[id].Width / 2)
                        {
                            Canvas.SetLeft(MalpaList[id], Canvas.GetLeft(MalpaList[id]) - 5);
                        }
                        else if (MainWindow.Punkty[PozycjeMalp[id]].X > Canvas.GetLeft(MalpaList[id]) + MalpaList[id].Width / 2)
                        {
                            Canvas.SetLeft(MalpaList[id], Canvas.GetLeft(MalpaList[id]) + 5);
                        }
                        else if (MainWindow.Punkty[PozycjeMalp[id]].Y < Canvas.GetTop(MalpaList[id]) + MalpaList[id].Height / 2)
                        {
                            Canvas.SetTop(MalpaList[id], Canvas.GetTop(MalpaList[id]) - 5);
                        }
                        else if (MainWindow.Punkty[PozycjeMalp[id]].Y > Canvas.GetTop(MalpaList[id]) + MalpaList[id].Height / 2)
                        {
                            Canvas.SetTop(MalpaList[id], Canvas.GetTop(MalpaList[id]) + 5);
                        }
                        else MessageBox.Show("jesli to sie pokazalo to cos sie mega zepsulo xd");
                    } else
                    {
                        PozycjeMalp[id]++;
                    }
                } catch (Exception)
                {
                    ListaTimerowRuchu[id].Stop();
                    MainWindow.Zycie -= 5;
                    MessageBox.Show(MainWindow.Zycie.ToString());
                    ListaTimerowRuchu.RemoveAt(id);
                    PozycjeMalp.RemoveAt(id);
                    PredkosciMalp.RemoveAt(id);
                    MainWindow.MyGame.Children.Remove(MalpaList[id]);
                    MalpaList.RemoveAt(id);
                }
            }

            ListaTimerowRuchu[id].Interval = TimeSpan.FromSeconds(1 / PredkosciMalp[id]);
            ListaTimerowRuchu[id].Tick += Malpy_Tick;

            ListaTimerowRuchu[id].Start();

        }

    }
}
