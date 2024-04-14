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
        // Zmienić \/\/\/
        static readonly int DlugPoczatkowy = 1000;

        public static Canvas MyGame;
        public static TextBlock TextPieniadze;
        public static TextBlock TextZycie;
        public static TextBlock TextDlug;
        public static int Pieniadze = 100;
        public static int Zycie = 100;
        public static int Dlug = 1000;
        public static int WybranyBalon;
        public static bool WyborAktywny = false;
        public MainWindow()
        {
            InitializeComponent();
            MyCanvas.Focus();

            MyGame = MyCanvas;
            TextPieniadze = PieniadzeText;
            TextZycie = ZycieText;
            TextDlug = DlugText;



            // \/\/\/ ODKOMENTOWAĆ NA KOŃCU!!!!! \/\/\/
            //WindowState = WindowState.Maximized;
            // /\/\/\ ODKOMENTOWAĆ NA KOŃCU!!!!! /\/\/\

            AktualizujWarotsci();
            MalpkiStart();
            ShowLog(); // Do wyświetlania dziwnych wartości
        }

        private void ShowLog()
        {
            InitializeComponent();
            Log SecondWindow = new Log();
            //SecondWindow.Owner = this;
            SecondWindow.Show();
        }

        private void LeftClick(object sender, MouseButtonEventArgs e)
        {
            if(WyborAktywny == true)
            {
                Point position = e.GetPosition(MyGame);
                Balony.CreateBalon(WybranyBalon, position);
                WyborAktywny = false;
            }
        }

        private void RightClick(object sender, MouseButtonEventArgs e)
        {
            SpawnMalpka(new Random().Next(0, 8));
            //SpawnMalpka(3);
        }

        void SpawnMalpka(int id)
        {
            Malpa malpa = new(id)
            {
                Width = 100,
                Height = 100
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

                malpa.RuchMalpy();
            }
        }

        public static void AktualizujWarotsci()
        {
            TextPieniadze.Text = Pieniadze.ToString();
            TextZycie.Text = Zycie.ToString();
            TextDlug.Text = $"{DlugPoczatkowy - Dlug}/{DlugPoczatkowy}";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz wyjść z gry?", 
                "Potwierdź wyjście z gry.",
                MessageBoxButton.YesNo,
                MessageBoxImage.None) == MessageBoxResult.Yes) Application.Current.Shutdown();
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void SelectBalon(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var Numer = button.FontSize;
            WybranyBalon = (int)Numer - 1;
            WyborAktywny = true;
            Log.Tekst += "wybrano balon: " + WybranyBalon.ToString() + "\n";
        }
    }
}