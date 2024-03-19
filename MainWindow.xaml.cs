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
        }
        private void Timer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void LeftClick(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(MyGame);
            Balony balony = new Balony();
            balony.CreateBalon(0, position.X, position.Y);
        }
    }
}