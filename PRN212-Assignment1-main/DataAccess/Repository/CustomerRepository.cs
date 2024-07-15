using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public void AddCustomer(Customer customer) => CustomerDAO.Instance.Add(customer);

        public void RemoveCustomer(Customer customer) => CustomerDAO.Instance.Delete(customer);

        public Customer GetCustomerByID(int id) => CustomerDAO.Instance.GetCustomerByID(id);

        public List<Customer> GetAllCustomers() => CustomerDAO.Instance.GetCustomersList().ToList();

        public void UpdateCustomer(Customer customer) => CustomerDAO.Instance.Update(customer);

        public Customer Login(string email, string password)
        {
            List<Customer> customers = GetAllCustomers();
            Customer? customer = customers.FirstOrDefault(c => (c.EmailAddress.Equals(email, StringComparison.OrdinalIgnoreCase) && c.Password.Equals(password)));
            return customer;
        }

        public List<Customer> FindByName(string fullName)
        {
            List<Customer> customers = GetAllCustomers();
            List<Customer> customer = customers.Where(c => c.CustomerFullName.ToLower().Contains(fullName.ToLower())).ToList();
            return customer;
        }
    }
}
