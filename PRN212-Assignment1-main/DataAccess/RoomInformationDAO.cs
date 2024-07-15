using BusinessObject;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RoomInformationDAO : BaseDAL
    {
        private static RoomInformationDAO instance = null;
        private static readonly object instanceLock = new object();
        private RoomInformationDAO() { }

        public static RoomInformationDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RoomInformationDAO();
                    }
                    return instance;
                }
            }
        }
        //------------------------------------------------
        public IEnumerable<RoomInformation> GetRoomInformationList()
        {
            SqlDataReader reader = null;
            string SQL = "SELECT * FROM RoomInformation";
            var roomInformations = new List<RoomInformation>();
            try
            {
                reader = DataProvider.GetDataReader(SQL, CommandType.Text, out connection);
                while (reader.Read())
                {
                    roomInformations.Add(new RoomInformation()
                    {
                        RoomID = reader.GetInt32("RoomID"),
                        RoomNumber = reader.GetString("RoomNumber"),
                        RoomDescription = reader.GetString("RoomDetailDescription"),
                        RoomMaxCapacity = reader.GetInt32("RoomMaxCapacity"),
                        RoomStatus = reader.GetByte("RoomStatus"),
                        RoomPricePerDate = reader.GetDecimal("RoomPricePerDay"),
                        RoomTypeID = reader.GetInt32("RoomTypeID")
                    });
                }
                return roomInformations;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                reader.Close();
                CloseConnection();
            }
        }
        //------------------------------------------------
        public RoomInformation GetRoomByID(int roomID)
        {
            SqlDataReader reader = null;
            string SQL = "SELECT * FROM RoomInformation WHERE RoomID = @RoomID";
            try
            {
                var param = DataProvider.CreateParameter("@RoomID", roomID, DbType.Int32);
                reader = DataProvider.GetDataReader(SQL, CommandType.Text, out connection, param);
                if (reader.Read())
                {
                    return new RoomInformation()
                    {
                        RoomID = reader.GetInt32("RoomID"),
                        RoomNumber = reader.GetString("RoomNumber"),
                        RoomDescription = reader.GetString("RoomDetailDescription"),
                        RoomMaxCapacity = reader.GetInt32("RoomMaxCapacity"),
                        RoomStatus = reader.GetByte("RoomStatus"),
                        RoomPricePerDate = reader.GetDecimal("RoomPricePerDay"),
                        RoomTypeID = reader.GetInt32("RoomTypeID")
                    };
                }
                return null; // If no record found with the given RoomID
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                reader.Close();
                CloseConnection();
            }
        }
        //------------------------------------------------
        public void Add(RoomInformation roomInformation)
        {
            try
            {
                // Check if the room already exists
                RoomInformation existingRoom = GetRoomByID(roomInformation.RoomID);
                if (existingRoom != null)
                {
                    throw new Exception("The room already exists.");
                }

                string SQLInsert = "INSERT INTO RoomInformation (RoomNumber, RoomDetailDescription, RoomMaxCapacity, RoomStatus, RoomPricePerDay, RoomTypeID) " +
                                   "VALUES (@RoomNumber, @RoomDescription, @RoomMaxCapacity, @RoomStatus, @RoomPricePerDate, @RoomTypeID)";
                var parameters = new List<SqlParameter>();
                parameters.Add(DataProvider.CreateParameter("@RoomNumber", 50, roomInformation.RoomNumber, DbType.String));
                parameters.Add(DataProvider.CreateParameter("@RoomDescription", 220, roomInformation.RoomDescription, DbType.String));
                parameters.Add(DataProvider.CreateParameter("@RoomMaxCapacity", roomInformation.RoomMaxCapacity, DbType.Int32));
                parameters.Add(DataProvider.CreateParameter("@RoomStatus", roomInformation.RoomStatus, DbType.Int32));
                parameters.Add(DataProvider.CreateParameter("@RoomPricePerDate", roomInformation.RoomPricePerDate, DbType.Decimal));
                parameters.Add(DataProvider.CreateParameter("@RoomTypeID", roomInformation.RoomTypeID, DbType.Int32));

                DataProvider.Insert(SQLInsert, CommandType.Text, parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        //------------------------------------------------
        public void Update(RoomInformation roomInformation)
        {
            try
            {
                // Check if the room exists
                RoomInformation existingRoom = GetRoomByID(roomInformation.RoomID);
                if (existingRoom == null)
                {
                    throw new Exception("The room does not exist.");
                }

                string SQLUpdate = "UPDATE RoomInformation SET RoomNumber = @RoomNumber, RoomDetailDescription = @RoomDescription, " +
                                   "RoomMaxCapacity = @RoomMaxCapacity, RoomStatus = @RoomStatus, RoomPricePerDay = @RoomPricePerDate, " +
                                   "RoomTypeID = @RoomTypeID WHERE RoomID = @RoomID";
                var parameters = new List<SqlParameter>();
                parameters.Add(DataProvider.CreateParameter("@RoomNumber", 50, roomInformation.RoomNumber, DbType.String));
                parameters.Add(DataProvider.CreateParameter("@RoomDescription", 220, roomInformation.RoomDescription, DbType.String));
                parameters.Add(DataProvider.CreateParameter("@RoomMaxCapacity", roomInformation.RoomMaxCapacity, DbType.Int32));
                parameters.Add(DataProvider.CreateParameter("@RoomStatus", roomInformation.RoomStatus, DbType.Int32));
                parameters.Add(DataProvider.CreateParameter("@RoomPricePerDate", roomInformation.RoomPricePerDate, DbType.Decimal));
                parameters.Add(DataProvider.CreateParameter("@RoomTypeID", roomInformation.RoomTypeID, DbType.Int32));
                parameters.Add(DataProvider.CreateParameter("@RoomID", roomInformation.RoomID, DbType.Int32));

                DataProvider.Update(SQLUpdate, CommandType.Text, parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        //------------------------------------------------
        public void Delete(RoomInformation roomInformation)
        {
            try
            {
                // Check if the room exists
                RoomInformation existingRoom = GetRoomByID(roomInformation.RoomID);
                if (existingRoom == null)
                {
                    throw new Exception("The room does not exist.");
                }

                string SQLDelete = "DELETE FROM RoomInformation WHERE RoomID = @RoomID";
                var parameters = new List<SqlParameter>();
                parameters.Add(DataProvider.CreateParameter("@RoomID", roomInformation.RoomID, DbType.Int32));

                DataProvider.Delete(SQLDelete, CommandType.Text, parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

    }
}
