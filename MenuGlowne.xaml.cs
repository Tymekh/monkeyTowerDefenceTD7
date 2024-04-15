using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace monkeyTowerDefenceTD7
{
    /// <summary>
    /// Logika interakcji dla klasy MenuGlowne.xaml
    /// </summary>
    public partial class MenuGlowne : Window
    {
        public MenuGlowne()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Gra = new MainWindow();
            Gra.Show();
            Close();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MinimiseWindow(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Tworcy(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Programiści:\n- T.D\n- A.B\n\nGraficy:\n- T.N\n- M.M");
        }
    }
}
