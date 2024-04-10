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
        public static int Zycie = 100;
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
            ;
        }
        private void LeftClick(object sender, MouseButtonEventArgs e)
        {
            Random rand = new Random();
            Point position = e.GetPosition(MyGame);
            //Balony balony = new Balony();
            Balony.CreateBalon(rand.Next(0,6), position.X, position.Y);
        }

        private void RightClick(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(MyGame);

            Malpa malpa = new()
            {
                Width = 50,
                Height = 50
            };
            Canvas.SetLeft(malpa, position.X - malpa.Width / 2);
            Canvas.SetTop(malpa, position.Y - malpa.Height / 2);
            MyGame.Children.Add(malpa);

            //Mouse_x = position.X;
            //Mouse_y = position.Y;
        }

        public static List<Point> Punkty = [];
        private void MyCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.M)
            {
                Punkty.Clear();

                foreach (Point p in Sciezka.Points)
                {
                    Punkty.Add(p);
                }
                foreach (Malpa malpa in MyGame.Children.OfType<Malpa>())
                {
                    malpa.Pozycja = 0;
                    Canvas.SetLeft(malpa, Punkty[0].X - malpa.Width / 2);
                    Canvas.SetTop(malpa, Punkty[0].Y - malpa.Height / 2);

                    malpa.TimerRuchu.Interval = TimeSpan.FromSeconds((double)1 / new Random().Next(10, 70));
                    malpa.TimerRuchu.Start();
                }
            }
        }
    }
}