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
        public MainWindow()
        {
            InitializeComponent();
            MyCanvas.Focus();
            MyGame = MyCanvas;
            //TimerStart();
            MalpkiStart();
            //ShowLog(); // Do wyświetlania dziwnych wartości
        }

        private void ShowLog()
        {
            InitializeComponent();
            Log SecondWindow = new Log();
            //SecondWindow.Owner = this;
            SecondWindow.Show();
        }

        //private void TimerStart()
        //{
        //    DispatcherTimer timer = new DispatcherTimer();
        //    timer.Tick += Timer_Tick;
        //    timer.Interval = TimeSpan.FromSeconds((double)1 / 30);
        //    timer.Start();
        //}

        private void LeftClick(object sender, MouseButtonEventArgs e)
        {
            Random rand = new Random();
            Point position = e.GetPosition(MyGame);
            //Balony balony = new Balony();
            Balony.CreateBalon(rand.Next(0,6), position);
        }

        private void RightClick(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(MyGame);

            Malpa malpa = new()
            {
                Width = 50,
                Height = 50
            };
            Canvas.SetLeft(malpa, Punkty[0].X - malpa.Width / 2);
            Canvas.SetTop(malpa, Punkty[0].Y - malpa.Height / 2);
            MyGame.Children.Add(malpa);
        }

        public static List<Point> Punkty = [];
        private static DispatcherTimer TimerRuchu = new();
        private void MalpkiStart()
        {
                Punkty.Clear();
                foreach (Point p in Sciezka.Points)
                {
                    Punkty.Add(p);
                }
                TimerRuchu.Interval = TimeSpan.FromSeconds((double)1 / 15);
                TimerRuchu.Tick += TimerRuchu_Tick;

                TimerRuchu.Start();
        }

        private void TimerRuchu_Tick(object? sender, EventArgs e)
        {
            for (int i = 0; i < MyGame.Children.OfType<Malpa>().Count(); i++)
            {
                Malpa malpa = MyGame.Children.OfType<Malpa>().ElementAt(i);

                malpa.Malpy_Tick();
            }
        }
    }
}