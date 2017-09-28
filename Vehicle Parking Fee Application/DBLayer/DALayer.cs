using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle_Parking_Fee_Application.Model;
using System.Configuration;

namespace Vehicle_Parking_Fee_Application.DBLayer
{
    public class DALayer
    {
        public static String ConnectionString;
        SqlConnection conn;
        private readonly ParkingFeeDB _pDBContext;
        enum Status { Open, Closed };
        enum AvailabilityStatus { Available, Occupied };

        public DALayer()
        {
            _pDBContext = new ParkingFeeDB();
        }

        public List<VehicleType> getAllVTypes()
        {
            return _pDBContext.VehicleType.ToList();
        }

        public List<VehicleDetails> getAllVDetails()
        {
            return _pDBContext.VehicleDetails.ToList();
        }

        public ParkingSpace FindNewParkingSpace(int floorNumber1, int floorNumber2)
        {
            ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            conn = new SqlConnection(ConnectionString);
            conn.Open();
            string query = "Select * from ParkingSpaces where AvailabilityStatus = 'Available'";
            SqlCommand cmd = new SqlCommand(query,conn);
            SqlDataReader dr = cmd.ExecuteReader();
            ParkingSpace obj = new ParkingSpace();
            while(dr.Read())
            {
                if(dr["AvailabilityStatus"].ToString() == "Available" && Convert.ToInt32(dr["floorNumber"]) == floorNumber1 || Convert.ToInt32(dr["floorNumber"]) == floorNumber2)
                {
                    obj.ParkingSpaceID = Convert.ToInt32(dr["ParkingSpaceID"]);
                    obj.floorNumber = Convert.ToInt32(dr["floorNumber"]);
                    obj.AvailabilityStatus = dr["AvailabilityStatus"].ToString();
                    obj.SlotName = dr["SlotName"].ToString();
                    return obj;
                }
            }
            return null;
        }

        public VehicleDetails AssignNewParkingSpace(VehicleDetails vDetailsObj)
        {
            ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            conn = new SqlConnection(ConnectionString);
            conn.Open();
            string query = "Insert into VehicleDetails (VehicleNumber,VehicleTypeID,DriverName) values (@VehicleNumber,@VehicleTypeID,@DriverName) select SCOPE_IDENTITY()";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@VehicleNumber", vDetailsObj.VehicleNumber.ToUpper());
            cmd.Parameters.AddWithValue("@VehicleTypeID", vDetailsObj.VehicleTypeID);
            cmd.Parameters.AddWithValue("@DriverName", vDetailsObj.DriverName);

            vDetailsObj.VehicleDetailsID = Convert.ToInt32(cmd.ExecuteScalar());

            return vDetailsObj;
        }

        public ParkingBookingHistory CreateParkingHistory(VehicleDetails vDetailsObj)
        {
            ParkingBookingHistory pobj = new ParkingBookingHistory();
            VehicleDetails vDetails = new VehicleDetails();
            ParkingSpace pSpace = new ParkingSpace();

            vDetails = AssignNewParkingSpace(vDetailsObj);
            if (vDetails.VehicleTypeID == 1)
                pSpace = FindNewParkingSpace(1, 0);
            else
                pSpace = FindNewParkingSpace(3,4);

            if(pSpace != null)
            {
                pobj.Status = Status.Open.ToString();
                pobj.ParkingSpaceID = pSpace.ParkingSpaceID;
                pobj.VehicleDetailsID = vDetails.VehicleDetailsID;
                pobj.TimeIn = DateTime.Now;
                pobj.TimeOut = DateTime.Now;
                pobj.OccupancyTime = 0;
                pobj.TotalParkingFee = 0;
                pobj.SlotName = pSpace.SlotName;
                ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                string query = "Insert into ParkingBookingHistories (Status,ParkingSpaceID,VehicleDetailsID,TimeIn,TimeOut,OccupancyTime,TotalParkingFee,SlotName) values (@Status,@ParkingSpaceID,@VehicleDetailsID,@TimeIn,@TimeOut,@OccupancyTime,@TotalParkingFee,@SlotName) select SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Status", pobj.Status);
                cmd.Parameters.AddWithValue("@ParkingSpaceID", pobj.ParkingSpaceID);
                cmd.Parameters.AddWithValue("@VehicleDetailsID", pobj.VehicleDetailsID);
                cmd.Parameters.AddWithValue("@TimeIn", pobj.TimeIn);
                cmd.Parameters.AddWithValue("@TimeOut", pobj.TimeOut);
                cmd.Parameters.AddWithValue("@OccupancyTime", pobj.OccupancyTime);
                cmd.Parameters.AddWithValue("@TotalParkingFee", pobj.TotalParkingFee);
                cmd.Parameters.AddWithValue("@SlotName", pSpace.SlotName);

                pobj.BookingID = Convert.ToInt32(cmd.ExecuteScalar());

                if (pobj.BookingID > 0)
                {
                    ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                    conn = new SqlConnection(ConnectionString);
                    conn.Open();
                    string Updatequery = "Update ParkingSpaces set AvailabilityStatus = @AvailabilityStatus where ParkingSpaceID = @ParkingSpaceID";
                    SqlCommand Updatecmd = new SqlCommand(Updatequery, conn);
                    Updatecmd.Parameters.AddWithValue("@AvailabilityStatus", AvailabilityStatus.Occupied.ToString());
                    Updatecmd.Parameters.AddWithValue("@ParkingSpaceID", pSpace.ParkingSpaceID);
                    Updatecmd.ExecuteNonQuery();
                    conn.Close();
                }

                return pobj;
            }
            else
            {
                return null;
            }
        }

        public ParkingBookingHistory VehicleCheckOut(VehicleDetails Obj)
        {
            var data = from post in _pDBContext.ParkingBookingHistory
                       join meta in _pDBContext.VehicleDetails
                       on post.VehicleDetailsID equals meta.VehicleDetailsID
                       where meta.VehicleNumber == Obj.VehicleNumber
                       select new { post };
            return _pDBContext.ParkingBookingHistory.Where(p => p.SlotName == Obj.VehicleNumber).FirstOrDefault();
        }
    }

    
    

    
}
