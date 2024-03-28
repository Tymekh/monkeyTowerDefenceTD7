using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace monkeyTowerDefenceTD7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Canvas MyGame;
        public static double Mouse_x, Mouse_y;
        public MainWindow()
        {
            InitializeComponent();
            MyCanvas.Focus();
            MyGame = MyCanvas;
            TimerStart();
        }
        private void TimerStart()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            tekst.Text = Bronie.Distance.ToString();
        }
        private void LeftClick(object sender, MouseButtonEventArgs e)
        {
            Random rand = new Random();
            Point position = e.GetPosition(MyGame);
            Balony balony = new Balony();
            balony.CreateBalon(rand.Next(0,6), position.X, position.Y);
        }

        private void RightClick(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(MyGame);
            Malpy malpy = new Malpy();
            malpy.CreateMalpa(position.X, position.Y);
        }

        private void MyCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            List<Point> Punkty = new List<Point>();
            List<int> Pozycje = Malpy.PozycjeMalp.Slice(0, Malpy.PozycjeMalp.Count);

            if (e.Key == Key.M)
            { 
                Punkty.Clear();

                foreach (Point p in Sciezka.Points)
                {
                    Punkty.Add(p);
                }
                for (int i = 0; i < Malpy.MalpaList.Count; i++)
                {
                    Malpy.MalpaNaSciezce(i, Punkty[Pozycje[i]].X, Punkty[Pozycje[i]].Y);
                    Malpy.PozycjeMalp[i]++;
                }
            }
        }
    }
}