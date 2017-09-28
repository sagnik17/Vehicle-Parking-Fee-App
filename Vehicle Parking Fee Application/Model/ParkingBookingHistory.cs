using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Vehicle_Parking_Fee_Application.Model;

namespace Vehicle_Parking_Fee_Application.Model
{
    public class ParkingBookingHistory
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingID { get; set; }
        public string Status { get; set; }
        public int ParkingSpaceID { get; set; }
        [ForeignKey("ParkingSpaceID")]
        public virtual ParkingSpace ParkingSpace { get; set; }
        public int VehicleDetailsID { get; set; }
        [ForeignKey("VehicleDetailsID")]
        public virtual VehicleDetails VehicleDetails { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public TimeSpan OccupancyTime { get; set; }
        public int TotalParkingFee { get; set; }
        public string SlotName { get; set; }
    }
}
