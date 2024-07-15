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
    public class RoomTypeDAO : BaseDAL
    {
        private static RoomTypeDAO instance = null;
        private static readonly object instanceLock = new object();
        private RoomTypeDAO() { }

        public static RoomTypeDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RoomTypeDAO();
                    }
                    return instance;
                }
            }
        }
        //--------------------------------------
        public IEnumerable<RoomType> GetRoomTypesList()
        {
            SqlDataReader reader = null;
            string SQL = "SELECT * FROM RoomType";
            var roomTypes = new List<RoomType>();
            try
            {
                reader = DataProvider.GetDataReader(SQL, CommandType.Text, out connection);
                while (reader.Read())
                {
                    roomTypes.Add(new RoomType()
                    {
                        RoomTypeID = reader.GetInt32("RoomTypeID"),
                        RoomTypeName = reader.GetString("RoomTypeName"),
                        TypeDescription = reader.GetString("TypeDescription"),
                        TypeNote = reader.GetString("TypeNote")
                    });
                }
                return roomTypes;
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
    }
}
