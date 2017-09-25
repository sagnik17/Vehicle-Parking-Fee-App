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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Vehicle_Parking_Fee_Application.View;

namespace Vehicle_Parking_Fee_Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void pageload1(Page pageToLoad)
        {
            MainFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            MainFrame.Content = pageToLoad;
        }

        private void VehicleCheckIn_Click(object sender, RoutedEventArgs e)
        {
            VehicleCheckIn v = new View.VehicleCheckIn();
            pageload1(v);
        }
    }
}
