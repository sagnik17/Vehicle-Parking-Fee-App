using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle_Parking_Fee_Application.Model;
using Vehicle_Parking_Fee_Application.DBLayer;
using System.Windows.Input;

namespace Vehicle_Parking_Fee_Application.ViewModel
{
    public class ParkingViewModel : ObservableObject
    {
        private List<VehicleType> _vtype;
        private string status;
        public VehicleType _svtype;
        private DALayer _dbLayerObj;
        private string _driverName;
        private string _vehicleNumber;
        private List<VehicleDetails> _vDetails;
        private VehicleDetails _svDetails;



        public string Message
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                NotifyPropertyChanged();
            }
        }
        public String VehicleNumber
        {
            get { return _vehicleNumber; }
            set
            {
                if (_vehicleNumber != value)
                {
                    _vehicleNumber = value;
                    NotifyPropertyChanged("VehicleNumber");
                }
            }
        }
        public String DriverName
        {
            get { return _driverName; }
            set
            {
                if (_driverName != value)
                {
                    _driverName = value;
                    
                    NotifyPropertyChanged("DriverName");
                }
            }
        }


        public List<VehicleDetails> VDetails
        {
            get { return _vDetails; }
            set { _vDetails = value; }
        }

        public VehicleDetails svDetails
        {
            get { return _svDetails; }
            set
            {
                _svDetails = value;
                DropDownChange(svDetails);
                NotifyPropertyChanged();
            }
        }


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

        }


        public ParkingViewModel(DALayer _dbObj)
        {
            getVehicleTypes();
            getVehicleDetails();
            //this._dbLayerObj = _dbLayerObj;
        }

        public void getVehicleTypes()
        {
            VType = new List<VehicleType>();
            _dbLayerObj = new DALayer();
            VType = _dbLayerObj.getAllVTypes();
        }

        public void getVehicleDetails()
        {
            VDetails = new List<VehicleDetails>();
            _dbLayerObj = new DALayer();
            VDetails = _dbLayerObj.getAllVDetails();
        }

        public ICommand GetParkingSpace
        {
            get
            {
                return new ActionCommand(p => AssignParkingSpace(SVType));
            }
        }

        public void AssignParkingSpace(VehicleType SVType)
        {
            if (SVType != null && DriverName != null && VehicleNumber != null)
            {
                VehicleDetails vDetailsObj = new VehicleDetails();
                ParkingBookingHistory pObj = new ParkingBookingHistory();
                vDetailsObj.DriverName = DriverName;
                vDetailsObj.VehicleNumber = VehicleNumber;
                vDetailsObj.VehicleTypeID = SVType.VehicleTypeID;
                pObj = _dbLayerObj.CreateParkingHistory(vDetailsObj);
                Message = "Parking Space Assigned is : " + pObj.SlotName;
            }
            else
            {
                Message = "All Fields are compulsory";
            }
        }

        public void DropDownChange(VehicleDetails svDetails)
        {
            _dbLayerObj = new DALayer();
            _dbLayerObj.VehicleCheckOut(svDetails);
        }

    }
}
