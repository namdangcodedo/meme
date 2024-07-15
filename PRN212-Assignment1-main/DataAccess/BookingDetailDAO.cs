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
    public class BookingDetailDAO : BaseDAL
    {
        private static BookingDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        private BookingDetailDAO() { }

        public static BookingDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BookingDetailDAO();
                    }
                    return instance;
                }
            }
        }
        //------------------------------
        public IEnumerable<BookingDetail> GetBookingDetailsList()
        {
            SqlDataReader reader = null;
            string SQL = "SELECT * FROM BookingDetail";
            var bookingDetails = new List<BookingDetail>();
            try
            {
                reader = DataProvider.GetDataReader(SQL, CommandType.Text, out connection);
                while (reader.Read())
                {
                    bookingDetails.Add(new BookingDetail()
                    {
                        BookingReservationID = reader.GetInt32("BookingReservationID"),
                        RoomID = reader.GetInt32("RoomID"),
                        StartDate = reader.GetDateTime("StartDate"),
                        EndDate = reader.GetDateTime("EndDate"),
                        ActualPrice = reader.GetDecimal("ActualPrice")
                    });
                }
                return bookingDetails;
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
        //------------------------------
        public BookingDetail GetBookingDetailsByBookingReservationIDAndRoomID(int bookingReservationID, int roomID)
        {
            SqlDataReader reader = null;
            string SQL = "SELECT * FROM BookingDetail WHERE BookingReservationID = @BookingReservationID AND RoomID = @RoomID";
            var bookingDetails = new BookingDetail();
            try
            {
                var paramReservationID = DataProvider.CreateParameter("@BookingReservationID", bookingReservationID, DbType.Int32);
                var paramRoomID = DataProvider.CreateParameter("@RoomID", roomID, DbType.Int32);
                reader = DataProvider.GetDataReader(SQL, CommandType.Text, out connection, paramReservationID, paramRoomID);
                if (reader.Read())
                {
                    bookingDetails = new BookingDetail()
                    {
                        BookingReservationID = reader.GetInt32("BookingReservationID"),
                        RoomID = reader.GetInt32("RoomID"),
                        StartDate = reader.GetDateTime("StartDate"),
                        EndDate = reader.GetDateTime("EndDate"),
                        ActualPrice = reader.GetDecimal("ActualPrice")
                    };
                }
                return bookingDetails;
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
        //------------------------------
        public void Add(BookingDetail bookingDetail)
        {
            try
            {
                // Check if the booking detail already exists
                BookingDetail existingBookingDetail = GetBookingDetailsByBookingReservationIDAndRoomID(bookingDetail.BookingReservationID, bookingDetail.RoomID);

                if (existingBookingDetail == null)
                {
                    string SQLInsert = "INSERT INTO BookingDetail (BookingReservationID, RoomID, StartDate, EndDate, ActualPrice) " +
                                       "VALUES (@BookingReservationID, @RoomID, @StartDate, @EndDate, @ActualPrice)";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(DataProvider.CreateParameter("@BookingReservationID", bookingDetail.BookingReservationID, DbType.Int32));
                    parameters.Add(DataProvider.CreateParameter("@RoomID", bookingDetail.RoomID, DbType.Int32));
                    parameters.Add(DataProvider.CreateParameter("@StartDate", bookingDetail.StartDate, DbType.DateTime));
                    parameters.Add(DataProvider.CreateParameter("@EndDate", bookingDetail.EndDate, DbType.DateTime));
                    parameters.Add(DataProvider.CreateParameter("@ActualPrice", bookingDetail.ActualPrice, DbType.Decimal));

                    DataProvider.Insert(SQLInsert, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("The booking detail already exists.");
                }
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
        //------------------------------
        public void Delete(BookingDetail bookingDetail)
        {
            try
            {
                // Check if the booking detail exists
                BookingDetail existingBookingDetail = GetBookingDetailsByBookingReservationIDAndRoomID(bookingDetail.BookingReservationID, bookingDetail.RoomID);

                if (existingBookingDetail != null)
                {
                    string SQLDelete = "DELETE FROM BookingDetail WHERE BookingReservationID = @BookingReservationID AND RoomID = @RoomID";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(DataProvider.CreateParameter("@BookingReservationID", bookingDetail.BookingReservationID, DbType.Int32));
                    parameters.Add(DataProvider.CreateParameter("@RoomID", bookingDetail.RoomID, DbType.Int32));

                    DataProvider.Delete(SQLDelete, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("The booking detail does not exist.");
                }
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
        //------------------------------
        public void Update(BookingDetail bookingDetail)
        {
            try
            {
                // Check if the booking detail exists
                BookingDetail existingBookingDetail = GetBookingDetailsByBookingReservationIDAndRoomID(bookingDetail.BookingReservationID, bookingDetail.RoomID);

                if (existingBookingDetail != null)
                {
                    string SQLUpdate = "UPDATE BookingDetail SET StartDate = @StartDate, EndDate = @EndDate, ActualPrice = @ActualPrice " +
                                       "WHERE BookingReservationID = @BookingReservationID AND RoomID = @RoomID";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(DataProvider.CreateParameter("@StartDate", bookingDetail.StartDate, DbType.DateTime));
                    parameters.Add(DataProvider.CreateParameter("@EndDate", bookingDetail.EndDate, DbType.DateTime));
                    parameters.Add(DataProvider.CreateParameter("@ActualPrice", bookingDetail.ActualPrice, DbType.Decimal));
                    parameters.Add(DataProvider.CreateParameter("@BookingReservationID", bookingDetail.BookingReservationID, DbType.Int32));
                    parameters.Add(DataProvider.CreateParameter("@RoomID", bookingDetail.RoomID, DbType.Int32));

                    DataProvider.Update(SQLUpdate, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("The booking detail does not exist.");
                }
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
