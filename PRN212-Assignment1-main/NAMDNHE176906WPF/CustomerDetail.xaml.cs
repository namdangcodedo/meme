using BusinessObject;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NAMDNHE176906WPF
{
    /// <summary>
    /// Interaction logic for CustomerDetail.xaml
    /// </summary>
    public partial class CustomerDetail : Window
    {
        private ICustomerRepository customerRepository;

        public CustomerDetail()
        {
            InitializeComponent();
            customerRepository = new CustomerRepository();
        }
        //--------------------------------
        public bool InsertOrUpdate {  get; set; }
        public Customer CustomerInfo { get; set; }

        private void LoadStatus()
        {
            var statuses = new List<Status>()
            {
                new Status { StatusID = 1, StatusName = "Active" },
                new Status { StatusID = 2, StatusName = "Inactive" }
            };

            cboStatus.ItemsSource = statuses;
            cboStatus.DisplayMemberPath = "StatusName";
            cboStatus.SelectedValuePath = "StatusID";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadStatus();
            cboStatus.SelectedIndex = 0;
            if (InsertOrUpdate)
            {
                txtCustomerID.Text = CustomerInfo.CustomerID.ToString();
                txtFullName.Text = CustomerInfo.CustomerFullName;
                txtEmail.Text = CustomerInfo.EmailAddress;
                txtTelephone.Text = CustomerInfo.Telephone;
                txtPassword.Text = CustomerInfo.Password;
                cboStatus.SelectedValue = CustomerInfo.CustomerStatus;
                dateBod.SelectedDate = CustomerInfo.CustomerBirthday;
                dateBod.DisplayDate = CustomerInfo.CustomerBirthday;
            }
        }

        private bool ValidateCustomer(Customer customer)
        {
            if (string.IsNullOrEmpty(customer.CustomerFullName))
            {
                MessageBox.Show("FullName cannot be empty!", "Validation Error");
                return false;
            }

            if (string.IsNullOrEmpty(customer.Telephone))
            {
                MessageBox.Show("Telephone cannot be empty!", "Validation Error");
                return false;
            }
            else
            {
                // Validate telephone number format (example: 10-15 digits)
                string phonePattern = @"^\d{10}$";
                if (!Regex.IsMatch(customer.Telephone, phonePattern))
                {
                    MessageBox.Show("Telephone number is not valid!", "Validation Error");
                    return false;
                }
            }

            if (string.IsNullOrEmpty(customer.EmailAddress))
            {
                MessageBox.Show("Email cannot be empty!", "Validation Error");
                return false;
            }
            else
            {
                // Validate email format
                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(customer.EmailAddress, emailPattern))
                {
                    MessageBox.Show("Email address is not valid!", "Validation Error");
                    return false;
                }
            }

            if (customer.CustomerBirthday == DateTime.Now)
            {
                MessageBox.Show("Please select a valid birthday!", "Validation Error");
                return false;
            }

            // Check if the customer is at least 18 years old
            var age = DateTime.Now.Year - customer.CustomerBirthday.Year;
            if (customer.CustomerBirthday > DateTime.Now.AddYears(-age)) age--;
            if (age < 18)
            {
                MessageBox.Show("Customer must be at least 18 years old!", "Validation Error");
                return false;
            }

            if (string.IsNullOrEmpty(customer.Password))
            {
                MessageBox.Show("Password cannot be empty!", "Validation Error");
                return false;
            }

            if (customer.CustomerStatus == 0) // Assuming 0 is an invalid status
            {
                MessageBox.Show("Please select a valid status!", "Validation Error");
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string fullName = txtFullName.Text;
                string telephone = txtTelephone.Text;
                string email = txtEmail.Text;
                DateTime birthday = dateBod.SelectedDate ?? DateTime.Now;
                string password = txtPassword.Text;
                int status = (int)cboStatus.SelectedValue;

                Customer customer = new Customer
                {
                    CustomerFullName = fullName,
                    Telephone = telephone,
                    EmailAddress = email,
                    CustomerBirthday = birthday,
                    Password = password,
                    CustomerStatus = status
                };

                if (!ValidateCustomer(customer))
                {
                    return;
                }

                if (InsertOrUpdate)
                {
                    int customerID = int.Parse(txtCustomerID.Text);
                    customer.CustomerID = customerID;
                    customerRepository.UpdateCustomer(customer);
                    MessageBox.Show("Saved successfully!", "Success");
                }
                else
                {
                    customerRepository.AddCustomer(customer);
                    MessageBox.Show("Saved successfully!", "Success");
                }

                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving customer: {ex.Message}", "Error");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) => Close();
    }
}
