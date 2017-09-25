using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle_Parking_Fee_Application.Model;
using Vehicle_Parking_Fee_Application.DBLayer;

namespace Vehicle_Parking_Fee_Application.ViewModel
{
    public class ParkingViewModel : ObservableObject
    {
        public List<VehicleType> _vtype;
        public VehicleType _svtype;
        private DALayer _dbLayerObj;

        public List<VehicleType> VType
        {
            get { return _vtype; }
            set { _vtype = value; }
        }

        public VehicleType SVType
        {
            get { return _svtype; }
            set { _svtype = value; NotifyPropertyChanged(); }
        }

        public ParkingViewModel() : this(new DALayer())
        {
            //Message = "";
            //GetCustomerList();
            //getFoodOrderItems();
        }


        public ParkingViewModel(DALayer _dbObj)
        {
            getVehicleTypes();
            //this._dbLayerObj = _dbLayerObj;
        }

        public void getVehicleTypes()
        {
            VType = new List<VehicleType>();
            _dbLayerObj = new DALayer();
            VType = _dbLayerObj.getAllVTypes();
        }


    }
}
