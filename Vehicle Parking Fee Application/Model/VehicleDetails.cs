using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Vehicle_Parking_Fee_Application.Model
{
    public class VehicleDetails
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehicleDetailsID { get; set; }
        public String VehicleNumber { get; set; }
        public int VehicleTypeID { get; set; }
        public string DriverName { get; set; }

    }
}
