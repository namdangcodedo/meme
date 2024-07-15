using BusinessObject;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CustomerDAO : BaseDAL
    {
        private static CustomerDAO instance = null;
        private static readonly object instanceLock = new object();
        private CustomerDAO() { }
        public static CustomerDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CustomerDAO();
                    }
                    return instance;
                }
            }
        }
        //---------------------------------
        public IEnumerable<Customer> GetCustomersList()
        {
            SqlDataReader reader = null;
            string SQL = "select * from Customer";
            var customers = new List<Customer>();
            try
            {
                reader = DataProvider.GetDataReader(SQL, CommandType.Text, out connection);
                while (reader.Read())
                {
                    customers.Add(new Customer()
                    {
                        CustomerID = reader.GetInt32("CustomerID"),
                        CustomerFullName = reader.GetString("CustomerFullName"),
                        CustomerBirthday = reader.GetDateTime("CustomerBirthday"),
                        Telephone = reader.GetString("Telephone"),
                        EmailAddress = reader.GetString("EmailAddress"),
                        Password = reader.GetString("Password"),
                        CustomerStatus = reader.GetByte("CustomerStatus")
                    });
                }
                return customers;
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
       
        public Customer GetCustomerByID(int id)
        {
            SqlDataReader reader = null;
            string SQL = "select * from Customer where CustomerID = @CustomerID";
            Customer customer = null;
            try
            {
                var param = DataProvider.CreateParameter("@CustomerID", id, DbType.Int32);
                reader = DataProvider.GetDataReader(SQL, CommandType.Text, out connection, param);
                if (reader.Read())
                {
                    customer = new Customer()
                    {
                        CustomerID = reader.GetInt32("CustomerID"),
                        CustomerFullName = reader.GetString("CustomerFullName"),
                        CustomerBirthday = reader.GetDateTime("CustomerBirthday"),
                        Telephone = reader.GetString("Telephone"),
                        EmailAddress = reader.GetString("EmailAddress"),
                        Password = reader.GetString("Password"),
                        CustomerStatus = reader.GetByte("CustomerStatus")
                    };
                }
                return customer;
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
        public void Add(Customer customer)
        {
            try
            {
                string SQLInsert = "Insert Customer values(@CustomerFullName,@Telephone,@EmailAddress,@CustomerBirthday,@CustomerStatus,@Password)";
                var parameters = new List<SqlParameter>();
                string date = customer.CustomerBirthday.ToString("yyyy-MM-dd");
                parameters.Add(DataProvider.CreateParameter("@CustomerFullName", 50, customer.CustomerFullName, DbType.String));
                parameters.Add(DataProvider.CreateParameter("@Telephone", 12, customer.Telephone, DbType.String));
                parameters.Add(DataProvider.CreateParameter("@EmailAddress", 50, customer.EmailAddress, DbType.String));
                parameters.Add(DataProvider.CreateParameter("@CustomerBirthday", date, DbType.DateTime));
                parameters.Add(DataProvider.CreateParameter("@CustomerStatus", customer.CustomerStatus, DbType.Int32));
                parameters.Add(DataProvider.CreateParameter("@Password", 50, customer.Password, DbType.String));
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
        //---------------------------------
        public void Update(Customer customer)
        {
            try
            {
                Customer cus = GetCustomerByID(customer.CustomerID);
                if (cus != null)
                {
                    string SQLInsert = "Update Customer set CustomerFullName = @CustomerFullName,Telephone = @Telephone,EmailAddress=@EmailAddress," +
                        "CustomerBirthday=@CustomerBirthday,CustomerStatus=@CustomerStatus,Password=@Password where CustomerID = @CustomerID";
                    var parameters = new List<SqlParameter>();
                    string date = customer.CustomerBirthday.ToString("yyyy-MM-dd");
                    parameters.Add(DataProvider.CreateParameter("@CustomerID", customer.CustomerID, DbType.Int32));
                    parameters.Add(DataProvider.CreateParameter("@CustomerFullName", 50, customer.CustomerFullName, DbType.String));
                    parameters.Add(DataProvider.CreateParameter("@Telephone", 12, customer.Telephone, DbType.String));
                    parameters.Add(DataProvider.CreateParameter("@EmailAddress", 50, customer.EmailAddress, DbType.String));
                    parameters.Add(DataProvider.CreateParameter("@CustomerBirthday", date, DbType.DateTime));
                    parameters.Add(DataProvider.CreateParameter("@CustomerStatus", customer.CustomerStatus, DbType.Int32));
                    parameters.Add(DataProvider.CreateParameter("@Password", 50, customer.Password, DbType.String));
                    DataProvider.Update(SQLInsert, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("The customer does not exist.");
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
        //---------------------------------
        public void Delete(Customer customer)
        {
            try
            {
                Customer cus = GetCustomerByID(customer.CustomerID);
                if (cus != null)
                {
                    string SQLInsert = "Delete Customer where CustomerID = @CustomerID";
                    var param = DataProvider.CreateParameter("@CustomerID", customer.CustomerID, DbType.Int32);
                    DataProvider.Delete(SQLInsert, CommandType.Text, param);
                }
                else
                {
                    throw new Exception("The customer does not exist.");
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
