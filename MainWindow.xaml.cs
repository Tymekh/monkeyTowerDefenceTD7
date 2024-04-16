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
        public static TextBlock TextPieniadze;
        public static TextBlock TextZycie;
        public static TextBlock TextDlug;
        public static TextBlock TextFala;
        public static int Pieniadze = 100;
        public static int Zycie = 100;
        public static int Dlug = 1000;
        private static int NumerFali = 0;
        public static int WybranyBalon;
        public static bool WyborAktywny = false;
        List<int> WartosciBalonow = new List<int>() { 100, 150, 250, 0, 200, 150 };
        private static bool CzyKoniec = false;

        public MainWindow()
        {
            InitializeComponent();
            MyCanvas.Focus();

            MyGame = MyCanvas;
            TextPieniadze = PieniadzeText;
            TextZycie = ZycieText;
            TextDlug = DlugText;
            TextFala = FalaText;

            TimerPoczatkowy.Interval = TimeSpan.FromSeconds(10);
            TimerMiedzyFalami.Interval = TimeSpan.FromSeconds(10);
            TimerMiedzySpawnami.Interval = TimeSpan.FromSeconds(1);

            TimerPoczatkowy.Tick += TimerPoczatkowy_Tick;
            TimerMiedzyFalami.Tick += TimerMiedzyFalami_Tick;
            TimerMiedzySpawnami.Tick += TimerMiedzySpawnami_Tick;

            TimerPoczatkowy.Start();

            // \/\/\/ ODKOMENTOWAĆ NA KOŃCU!!!!! \/\/\/
            //WindowState = WindowState.Maximized;
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
                Pieniadze -= WartosciBalonow[WybranyBalon];
                WyborAktywny = false;
                AktualizujWarotsci();
            }
        }

        private void RightClick(object sender, MouseButtonEventArgs e)
        {
            NastepnaFala();
            //SpawnMalpka(new Random().Next(0, 8));
            //SpawnMalpka(6);
            ;
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

            if (NumerFali >= 12)   // Szansa na pojawienie mutantów od fali 12
            {
                SpawnMalpka(random.Next(0, 7));
            }
            else if (NumerFali >= 10)   // Szansa na pojawienie małp w zbroi od fali 10
            {
                SpawnMalpka(random.Next(0, 6));
            }
            else if (NumerFali >= 8)   // Szansa na pojawienie małp matek od fali 8
            {
                SpawnMalpka(random.Next(0, 5));
            }
            else if (NumerFali >= 6)   // Szansa na pojawienie małp z hełmem od fali 6
            {
                SpawnMalpka(random.Next(0, 4));
            }
            else if (NumerFali >= 4)   // Szansa na pojawienie czarnych malp od fali 4
            {
                SpawnMalpka(random.Next(0, 3));
            }
            else if (NumerFali >= 2)   // Szansa na pojawienie małp albinosów od fali 2
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
            TextDlug.Text = $"{Dlug}";
            TextFala.Text = $"Fala: {NumerFali}";

            if (Zycie <= 0) KoniecGry(false);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz wyjść z gry?", 
                "Potwierdź wyjście z gry.",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes) Application.Current.Shutdown();
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void SelectBalon(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var Numer = button.FontSize;
            if((Pieniadze >= WartosciBalonow[(int)Numer - 1]) || (WartosciBalonow[(int)Numer - 1] == 0))
            {
                WybranyBalon = (int)Numer - 1;
                WyborAktywny = true;
            }
            else
            {
                MessageBox.Show("za malo pieniedzy");
            }
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

        void NastepnaFala()
        {
            DlugoscFali = new Random().Next(10, 15);
            if (NumerFali >= 25)
            {
                if (TimerMiedzySpawnami.Interval == TimeSpan.FromSeconds(1)) TimerMiedzySpawnami.Interval = TimeSpan.FromSeconds(0.5);
                DlugoscFali *= 2;
            }
            TimerMiedzyFalami.Stop();
            TimerMiedzySpawnami.Start();
            Dlug = (int)(Dlug * 1.05);
            NumerFali++;
            AktualizujWarotsci();

        }

        private void TimerMiedzyFalami_Tick(object? sender, EventArgs e)
        {
            NastepnaFala();
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
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Pieniadze >= 100)
            {
                Pieniadze -= 100;
                Dlug -= Dlug < 100 ? Dlug : 100;
                AktualizujWarotsci();
            }
            if (Dlug <= 0)
            {
                KoniecGry(true);
            }
        }

        private static void KoniecGry(bool CzyWygrana)
        {
            if (!CzyKoniec)
            {
                if (CzyWygrana)
                {
                    MessageBox.Show($"Gratulację, Udało ci się spłacic twój dług na fali {NumerFali}!!!");
                    Application.Current.Shutdown();
                }
                else
                {
                    MessageBox.Show($"Przegrałeś/aś na fali {NumerFali}!!!");
                    Application.Current.Shutdown();
                }
                CzyKoniec = true;
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