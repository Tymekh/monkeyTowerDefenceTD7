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
        static readonly int DlugPoczatkowy = 10000;

        public static Canvas MyGame;
        public static TextBlock TextPieniadze;
        public static TextBlock TextZycie;
        public static TextBlock TextDlug;
        public static int Pieniadze = 20000;
        public static int Zycie = 100;
        public static int Dlug = 10000;
        public static int WybranyBalon;
        public static bool WyborAktywny = false;
        private static int NumerFali = 0;

        public MainWindow()
        {
            InitializeComponent();
            MyCanvas.Focus();

            MyGame = MyCanvas;
            TextPieniadze = PieniadzeText;
            TextZycie = ZycieText;
            TextDlug = DlugText;

            TimerPoczatkowy.Interval = TimeSpan.FromSeconds(20);
            TimerMiedzyFalami.Interval = TimeSpan.FromSeconds(20);
            TimerMiedzySpawnami.Interval = TimeSpan.FromSeconds(2);

            TimerPoczatkowy.Tick += TimerPoczatkowy_Tick;
            TimerMiedzyFalami.Tick += TimerMiedzyFalami_Tick;
            TimerMiedzySpawnami.Tick += TimerMiedzySpawnami_Tick;

            TimerPoczatkowy.Start();


            // \/\/\/ ODKOMENTOWAĆ NA KOŃCU!!!!! \/\/\/
            WindowState = WindowState.Maximized;
            // /\/\/\ ODKOMENTOWAĆ NA KOŃCU!!!!! /\/\/\

            AktualizujWarotsci();
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
            //SpawnMalpka(6);
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

        private void SpawnZaleznieOdTrudnosci()
        {
            Random random = new();

            if (NumerFali >= 18)   // Szansa na pojawienie mutantów od fali 18
            {
                SpawnMalpka(random.Next(0, 7));
            }
            else if (NumerFali >= 15)   // Szansa na pojawienie małp w zbroi od fali 15
            {
                SpawnMalpka(random.Next(0, 6));
            }
            else if (NumerFali >= 12)   // Szansa na pojawienie małp matek od fali 12
            {
                SpawnMalpka(random.Next(0, 5));
            }
            else if (NumerFali >= 9)   // Szansa na pojawienie małp z hełmem od fali 9
            {
                SpawnMalpka(random.Next(0, 4));
            }
            else if (NumerFali >= 6)   // Szansa na pojawienie czarnych malp od fali 6
            {
                SpawnMalpka(random.Next(0, 3));
            }
            else if (NumerFali >= 3)   // Szansa na pojawienie małp albinosów od fali 3
            {
                SpawnMalpka(random.Next(0, 2));
            }
            else   // Pojawianie tylko zwykłych małp na pierwszych kilku falach
            {
                SpawnMalpka(0);
            }
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
                TimerRuchu.Interval = TimeSpan.FromSeconds((double)1 / 20);
                TimerRuchu.Tick += TimerRuchu_Tick;

                TimerRuchu.Start();
        }

        private void TimerRuchu_Tick(object? sender, EventArgs e)
        {
            for (int i = 0; i < MyGame.Children.OfType<Malpa>().Count(); i++)
            {
                MyGame.Children.OfType<Malpa>().ElementAt(i).RuchMalpy();
            }
        }

        public static void AktualizujWarotsci()
        {
            TextPieniadze.Text = Pieniadze.ToString();
            TextZycie.Text = Zycie.ToString();
            TextDlug.Text = $"{DlugPoczatkowy - Dlug}/{DlugPoczatkowy}";

            if (Zycie <= 0) KoniecGry(false);
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

        // Spawnowanie Malpek

        static DispatcherTimer TimerPoczatkowy = new();
        static DispatcherTimer TimerMiedzyFalami = new();
        static DispatcherTimer TimerMiedzySpawnami = new();
        int DlugoscFali;
        private void TimerPoczatkowy_Tick(object? sender, EventArgs e)
        {
            TimerPoczatkowy.Stop();
            TimerMiedzyFalami.Start();
        }


        private void TimerMiedzyFalami_Tick(object? sender, EventArgs e)
        {
            DlugoscFali = new Random().Next(10, 15);
            TimerMiedzyFalami.Stop();
            TimerMiedzySpawnami.Start();
        }
        private void TimerMiedzySpawnami_Tick(object? sender, EventArgs e)
        {
            if (DlugoscFali-- > 0)
            {
                SpawnZaleznieOdTrudnosci();
                TimerMiedzySpawnami.Start();
            }
            else
            {
                TimerMiedzySpawnami.Stop();
                TimerMiedzyFalami.Start();
                NumerFali++;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Pieniadze >= 100)
            {
                Pieniadze -= 100;
                Dlug -= 100;
                AktualizujWarotsci();
            }
            if (Dlug <= 0)
            {
                KoniecGry(true);
            }
        }

        private static void KoniecGry(bool CzyWygrana)
        {
            if (CzyWygrana)
            {
                MessageBox.Show("Gratulację, Wygrałeś/aś!!!");
                Application.Current.Shutdown();
            }
            else
            {
                MessageBox.Show("Przegrałeś/aś!!!");
                Application.Current.Shutdown();
            }
        }
    }
}
//⠀⠀⠀⠀⠀⠀⣖⢤⡀⠀⠀⠀⠀⠀⠀⠀
//⡐⠣⡄⠀⠀⠀⠑⡄⠙⠆⠀⠀⠀⠀⠀⠀
//⡇⠘⢄⡄⠂⠉⠀⡀⠀⢸⠀⠀⠀⠀⠀⠀
//⢃⡐⠁⠀⠀⠀⠀⠀⠀⡼⠆⠀⠀⢀⣴⠂
//⠀⢁⠀⠀⣠⡀⠀⠀⠀⠀⢸⢀⣴⣿⠏⠀
//⠀⠈⣿⡄⢹⣿⡄⠀⢀⡀⢠⣿⣿⠏⠀⠀
//⠀⠀⠈⢇⠀⢉⣥⣾⣿⣷⣿⣿⠏⠀⠀⠀
//⠀⠀⠀⠀⣽⣿⣿⣿⣿⣿⣿⣯⠱⡀⠀⠀
//⠀⠀⠀⠀⠙⠋⢻⣿⣿⣿⣿⣿⡄⡇⠀⠀
//⠀⠀⠀⠀⠀⠘⣼⣿⣿⣿⣿⣿⣷⡇⠀⠀
//⠀⠀⠀⠀⠀⢠⣿⣿⡿⠛⡿⣿⡇⡇⠀⠀
//⠀⠀⠀⠀⠠⠟⠋⠀⠈⠂⠌⠺⠇⠀⠀