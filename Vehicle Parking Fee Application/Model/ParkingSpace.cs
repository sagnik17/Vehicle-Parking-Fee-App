using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Vehicle_Parking_Fee_Application.Model
{
    public class ParkingSpace
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ParkingSpaceID { get; set; }
        public string SlotName { get; set; }
        public string AvailabilityStatus { get; set; }
        public int floorNumber { get; set; }
    }
}
