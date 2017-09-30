using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Parking_Fee_Application.Model
{
    public class sampleData : DropCreateDatabaseAlways<ParkingFeeDB>
    {
        enum AvailabilityStatus { Available, Occupied };
        enum Status { Open, Closed };

        protected override void Seed(ParkingFeeDB context)
        {
            try
            {

                for (int i = 1; i <= 205; i++)
                {
                    if (i < 10)
                    {
                        ParkingSpace obj = new ParkingSpace();
                        obj.SlotName = "F1-00" + i;
                        obj.floorNumber = 1;
                        obj.AvailabilityStatus = AvailabilityStatus.Available.ToString();
                        context.ParkingSpace.Add(obj);
                    }
                    else if (i < 100)
                    {
                        ParkingSpace obj = new ParkingSpace();
                        obj.SlotName = "F1-0" + i;
                        obj.floorNumber = 1;
                        obj.AvailabilityStatus = AvailabilityStatus.Available.ToString();
                        context.ParkingSpace.Add(obj);
                    }
                    else
                    {
                        ParkingSpace obj = new ParkingSpace();
                        obj.SlotName = "F1-" + i;
                        obj.floorNumber = 1;
                        obj.AvailabilityStatus = AvailabilityStatus.Available.ToString();
                        context.ParkingSpace.Add(obj);
                    }
                }


                for (int i = 1; i <= 100; i++)
                {
                    if (i < 10)
                    {
                        ParkingSpace obj = new ParkingSpace();
                        obj.SlotName = "F2-00" + i;
                        obj.floorNumber = 2;
                        obj.AvailabilityStatus = AvailabilityStatus.Available.ToString();
                        context.ParkingSpace.Add(obj);
                    }
                    else if (i < 100)
                    {
                        ParkingSpace obj = new ParkingSpace();
                        obj.SlotName = "F2-0" + i;
                        obj.floorNumber = 2;
                        obj.AvailabilityStatus = AvailabilityStatus.Available.ToString();
                        context.ParkingSpace.Add(obj);
                    }
                    else
                    {
                        ParkingSpace obj = new ParkingSpace();
                        obj.SlotName = "F2-" + i;
                        obj.floorNumber = 2;
                        obj.AvailabilityStatus = AvailabilityStatus.Available.ToString();
                        context.ParkingSpace.Add(obj);
                    }
                }

                for (int i = 1; i <= 100; i++)
                {
                    if (i < 10)
                    {
                        ParkingSpace obj = new ParkingSpace();
                        obj.SlotName = "F3-00" + i;
                        obj.floorNumber = 3;
                        obj.AvailabilityStatus = AvailabilityStatus.Available.ToString();
                        context.ParkingSpace.Add(obj);
                    }
                    else if (i < 100)
                    {
                        ParkingSpace obj = new ParkingSpace();
                        obj.SlotName = "F3-0" + i;
                        obj.floorNumber = 3;
                        obj.AvailabilityStatus = AvailabilityStatus.Available.ToString();
                        context.ParkingSpace.Add(obj);
                    }
                    else
                    {
                        ParkingSpace obj = new ParkingSpace();
                        obj.SlotName = "F3-" + i;
                        obj.floorNumber = 3;
                        obj.AvailabilityStatus = AvailabilityStatus.Available.ToString();
                        context.ParkingSpace.Add(obj);
                    }
                }

                List<VehicleType> VehicleTypes = new List<VehicleType>()
                {
                    new VehicleType { VehicleTypeID = 1, VehicleTypeName = "2 Wheeler" },
                    new VehicleType { VehicleTypeID = 2, VehicleTypeName = "4 Wheeler" },
                };

                foreach (VehicleType v in VehicleTypes)
                {
                    context.VehicleType.Add(v);
                }
               
                context.SaveChanges();
                base.Seed(context);



            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
