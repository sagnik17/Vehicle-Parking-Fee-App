using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;

namespace Vehicle_Parking_Fee_Application.Model
{
    public class ParkingFeeDB : DbContext
    {
        public ParkingFeeDB() : base("DBContext")
        {
            Database.SetInitializer(new sampleData());
        }
        public DbSet<ParkingSpace> ParkingSpace { get; set; }
        public DbSet<ParkingBookingHistory> ParkingBookingHistory { get; set; }
        public DbSet<VehicleDetails> VehicleDetails { get; set; }
        public DbSet<VehicleType> VehicleType { get; set; }
    }
}
