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
        private string _vehicleType;
        private DateTime _timein;
        private DateTime _timeout;
        private TimeSpan _parkingTime;
        public string _parkingSlot;
        private List<VehicleDetails> _vDetails;
        private VehicleDetails _svDetails;

        public string VehicleType
        {
            get
            {
                return _vehicleType;
            }
            set
            {
                _vehicleType = value;
                NotifyPropertyChanged();
            }
        }
        public TimeSpan ParkingTime
        {
            get
            {
                return _parkingTime;
            }
            set
            {
                _parkingTime = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime TimeOut
        {
            get
            {
                return _timeout;
            }
            set
            {
                _timeout = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime Timein
        {
            get
            {
                return _timein;
            }
            set
            {
                _timein = value;
                NotifyPropertyChanged();
            }
        }

        public string ParkingSlot
        {
            get
            {
                return _parkingSlot;
            }
            set
            {
                _parkingSlot = value;
                NotifyPropertyChanged();
            }
        }

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
            ParkingBookingHistory pbhObj = new ParkingBookingHistory();
            pbhObj = _dbLayerObj.VehicleCheckOut(svDetails);
            DriverName = pbhObj.VehicleDetails.DriverName;
            ParkingTime = (DateTime.Now - pbhObj.TimeIn).Duration();
            TimeOut = DateTime.Now;
            Timein = pbhObj.TimeIn;
            ParkingSlot = pbhObj.ParkingSpace.SlotName;
            VehicleType = pbhObj.VehicleDetails.VehicleType.VehicleTypeName;
        }

        public ICommand CheckOutVehicleCmd
        {
            get
            {
                return new ActionCommand(p => CheckOutVehicle());
            }
        }

        public void CheckOutVehicle()
        {
            _dbLayerObj = new DALayer();
            ParkingBookingHistory pbhObj = new ParkingBookingHistory();
            pbhObj = _dbLayerObj.VehicleCheckOut(svDetails);

            if (ParkingTime.Hours == 0 && svDetails.VehicleTypeID == 1)
            {
                Message = "Total Parking Fee is 20 \n" + "Parking Slot : " + ParkingSlot + " is Available";
                pbhObj.TotalParkingFee = 20;
            }
            else if (ParkingTime.Hours == 0 && svDetails.VehicleTypeID == 2)
            {
                Message = "Total Parking Fee is 40 \n" + "Parking Slot : " + ParkingSlot + " is Available";
                pbhObj.TotalParkingFee = 40;
            }
            else if (ParkingTime.Hours == 1 && svDetails.VehicleTypeID == 1)
            {
                Message = "Total Parking Fee is 40 \n" + "Parking Slot : " + ParkingSlot + " is Available";
                pbhObj.TotalParkingFee = 40;
            }
            else if (ParkingTime.Hours == 1 && svDetails.VehicleTypeID == 2)
            {
                Message = "Total Parking Fee is 50 \n" + "Parking Slot : " + ParkingSlot + " is Available";
                pbhObj.TotalParkingFee = 50;
            }
            else if (ParkingTime.Hours > 1 && svDetails.VehicleTypeID == 1)
            {
                Message = "Total Parking Fee is 60 \n" + "Parking Slot : " + ParkingSlot + " is Available";
                pbhObj.TotalParkingFee = 60;
            }
            else if (ParkingTime.Hours > 1 && svDetails.VehicleTypeID == 2)
            {
                Message = "Total Parking Fee is 70 \n" + "Parking Slot : " + ParkingSlot + " is Available";
                pbhObj.TotalParkingFee = 70;
            }

           
            pbhObj.TimeOut = TimeOut;
            pbhObj.OccupancyTime = ParkingTime;
            _dbLayerObj.CheckOutVehicle(pbhObj);

        }

    }
}
