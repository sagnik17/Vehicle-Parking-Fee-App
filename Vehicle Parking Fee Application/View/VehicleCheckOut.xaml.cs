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
using Vehicle_Parking_Fee_Application.ViewModel;

namespace Vehicle_Parking_Fee_Application.View
{
    /// <summary>
    /// Interaction logic for VehicleCheckOut.xaml
    /// </summary>
    public partial class VehicleCheckOut : Page
    {
        public VehicleCheckOut()
        {
            InitializeComponent();
            DataContext = new ParkingViewModel();
        }
    }
}
