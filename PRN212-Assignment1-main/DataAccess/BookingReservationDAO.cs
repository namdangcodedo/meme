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
    public class BookingReservationDAO : BaseDAL
    {
        private static BookingReservationDAO instance = null;
        private static readonly object instanceLock = new object();
        public BookingReservationDAO() { }

        public static BookingReservationDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BookingReservationDAO();
                    }
                    return instance;
                }
            }
        }
        //---------------------------------
        public IEnumerable<BookingReservation> GetBookingReservationsList()
        {
            SqlDataReader reader = null;
            string SQL = "SELECT * FROM BookingReservation";
            var bookingReservations = new List<BookingReservation>();
            try
            {
                reader = DataProvider.GetDataReader(SQL, CommandType.Text, out connection);
                while (reader.Read())
                {
                    bookingReservations.Add(new BookingReservation()
                    {
                        BookingReservationID = reader.GetInt32("BookingReservationID"),
                        BookingDate = reader.GetDateTime("BookingDate"),
                        TotalPrice = reader.GetDecimal("TotalPrice"),
                        CustomerID = reader.GetInt32("CustomerID"),
                        BookingStatus = reader.GetByte("BookingStatus")
                    });
                }
                return bookingReservations;
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
        public List<BookingReservation> GetBookingReservationsList1()
        {
            SqlDataReader reader = null;
            string SQL = "SELECT * FROM BookingReservation";
            var bookingReservations = new List<BookingReservation>();
            try
            {
                reader = DataProvider.GetDataReader(SQL, CommandType.Text, out connection);
                while (reader.Read())
                {
                    bookingReservations.Add(new BookingReservation()
                    {
                        BookingReservationID = reader.GetInt32("BookingReservationID"),
                        BookingDate = reader.GetDateTime("BookingDate"),
                        TotalPrice = reader.GetDecimal("TotalPrice"),
                        CustomerID = reader.GetInt32("CustomerID"),
                        BookingStatus = reader.GetByte("BookingStatus")
                    });
                }
                return bookingReservations;
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

        public int GetcountBookingReservationsList1()
        {
            SqlDataReader reader = null;
            string SQL = "SELECT COUNT(*) FROM BookingReservation";
            try
            {
                int count = 0;
                reader = DataProvider.GetDataReader(SQL, CommandType.Text, out connection);
                if (reader.Read())
                {
                    count = reader.GetInt32(0);
                }
                return count;
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
        //---------------------------------
        public BookingReservation GetBookingReservationByID(int bookingReservationID)
        {
            SqlDataReader reader = null;
            string SQL = "SELECT * FROM BookingReservation WHERE BookingReservationID = @BookingReservationID";
            try
            {
                var param = DataProvider.CreateParameter("@BookingReservationID", bookingReservationID, DbType.Int32);
                reader = DataProvider.GetDataReader(SQL, CommandType.Text, out connection, param);
                if (reader.Read())
                {
                    return new BookingReservation()
                    {
                        BookingReservationID = reader.GetInt32("BookingReservationID"),
                        BookingDate = reader.GetDateTime("BookingDate"),
                        TotalPrice = reader.GetDecimal("TotalPrice"),
                        CustomerID = reader.GetInt32("CustomerID"),
                        BookingStatus = reader.GetByte("BookingStatus")
                    };
                }
                return null; // If no record found with the given BookingReservationID
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

        //---------------------------------
        public bool Add(BookingReservation bookingReservation)
        {
            try
            {
                // Check if the booking reservation already exists
                BookingReservation existingReservation = GetBookingReservationByID(bookingReservation.BookingReservationID);
                if (existingReservation != null)
                {
                    throw new Exception("The booking reservation already exists.");
                }

                BookingReservationDAO dal = new BookingReservationDAO();
                int c = dal.GetcountBookingReservationsList1();
                c++;
                    string SQLInsert = "INSERT INTO BookingReservation (BookingReservationID,BookingDate, TotalPrice, CustomerID, BookingStatus) " +
                           "VALUES (@BookingReservationID,@BookingDate, @TotalPrice, @CustomerID, @BookingStatus)";
                var parameters = new List<SqlParameter>
                          {

               DataProvider.CreateParameter("@BookingReservationID", c,DbType.Int32),
              DataProvider.CreateParameter("@BookingDate", bookingReservation.BookingDate, DbType.DateTime),
               DataProvider.CreateParameter("@TotalPrice", bookingReservation.TotalPrice, DbType.Decimal),
              DataProvider.CreateParameter("@CustomerID", bookingReservation.CustomerID, DbType.Int32),
               DataProvider.CreateParameter("@BookingStatus", bookingReservation.BookingStatus, DbType.Byte)
        };

                DataProvider.Insert(SQLInsert, CommandType.Text, parameters.ToArray());
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                 return false;      
            }
            finally
            {
                CloseConnection();
            }
        }
        //---------------------------------
        public void Update(BookingReservation bookingReservation)
        {
            try
            {
                // Check if the booking reservation exists
                BookingReservation existingReservation = GetBookingReservationByID(bookingReservation.BookingReservationID);
                if (existingReservation == null)
                {
                    throw new Exception("The booking reservation does not exist.");
                }

                string SQLUpdate = "UPDATE BookingReservation SET BookingDate = @BookingDate, TotalPrice = @TotalPrice, " +
                                   "CustomerID = @CustomerID, BookingStatus = @BookingStatus WHERE BookingReservationID = @BookingReservationID";
                var parameters = new List<SqlParameter>();
                parameters.Add(DataProvider.CreateParameter("@BookingDate", bookingReservation.BookingDate, DbType.DateTime));
                parameters.Add(DataProvider.CreateParameter("@TotalPrice", bookingReservation.TotalPrice, DbType.Decimal));
                parameters.Add(DataProvider.CreateParameter("@CustomerID", bookingReservation.CustomerID, DbType.Int32));
                parameters.Add(DataProvider.CreateParameter("@BookingStatus", bookingReservation.BookingStatus, DbType.Byte));
                parameters.Add(DataProvider.CreateParameter("@BookingReservationID", bookingReservation.BookingReservationID, DbType.Int32));

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
        //---------------------------------
        public void Delete(BookingReservation bookingReservation)
        {
            try
            {
                // Check if the booking reservation exists
                BookingReservation existingReservation = GetBookingReservationByID(bookingReservation.BookingReservationID);
                if (existingReservation == null)
                {
                    throw new Exception("The booking reservation does not exist.");
                }

                string SQLDelete = "DELETE FROM BookingReservation WHERE BookingReservationID = @BookingReservationID";
                var parameters = new List<SqlParameter>();
                parameters.Add(DataProvider.CreateParameter("@BookingReservationID", bookingReservation.BookingReservationID, DbType.Int32));

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
