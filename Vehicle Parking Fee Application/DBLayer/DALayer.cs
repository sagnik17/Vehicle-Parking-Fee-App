using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle_Parking_Fee_Application.Model;

namespace Vehicle_Parking_Fee_Application.DBLayer
{
    public class DALayer
    {
        private readonly ParkingFeeDB _pDBContext;

        public DALayer()
        {
            _pDBContext = new ParkingFeeDB();
        }

        public List<VehicleType> getAllVTypes()
        {
            return _pDBContext.VehicleType.ToList();
        }
    }
}
