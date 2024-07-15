using BusinessObject;
using Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DangTMHE181967WPF
{

    /// <summary>
    /// Interaction logic for ManageCustomerWindow.xaml
    /// </summary>
    public partial class ManageCustomerWindow : Window
    {

        private ICustomerRepository customerRepository;

        public ManageCustomerWindow()
        {
            InitializeComponent();
            customerRepository = new CustomerRepository();
        }

        public void LoadCustomerList()
        {
            try
            {
                var customerList = customerRepository.GetAllCustomers();
                dgData.ItemsSource = customerList;
            } 
            catch(Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error on load list of customer");
            }
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid? dataGrid = sender as DataGrid;
            DataGridRow? row = (DataGridRow) dataGrid.ItemContainerGenerator.ContainerFromIndex(dgData.SelectedIndex);
            DataGridCell? rowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
            if(rowColumn != null)
            {
                int id = Int32.Parse(((TextBlock)rowColumn.Content).Text);
                Customer customer = customerRepository.GetCustomerByID(id);
                txtSearchCustomer.Text = customer.CustomerFullName;
                txtCustomerID.Text = customer.CustomerID.ToString();
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CustomerDetail customerDetail = new CustomerDetail()
                {
                    Title = "Insert Customer",
                    InsertOrUpdate = false
                };
                customerDetail.Closed += (s, args) => LoadCustomerList(); // Add this line to refresh the customer list after closing
                customerDetail.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtCustomerID.Text.Length > 0)
                {
                    int id = Int32.Parse(txtCustomerID.Text);
                    Customer customer = customerRepository.GetCustomerByID(id);

                    CustomerDetail customerDetail = new CustomerDetail()
                    {
                        Title = "Update Customer",
                        InsertOrUpdate = true,
                        CustomerInfo = customer
                    };
                    customerDetail.Closed += (s, args) => LoadCustomerList(); // Add this line to refresh the customer list after closing
                    customerDetail.ShowDialog();
                }
                else
                {
                    MessageBox.Show("You must select a customer!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtCustomerID.Text.Length > 0)
                {
                    Customer customer = new Customer();
                    customer.CustomerID = Int32.Parse(txtCustomerID.Text);
                    MessageBoxResult result = MessageBox
                        .Show($"Do you want to delete customer number {customer.CustomerID}?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        customerRepository.RemoveCustomer(customer);
                    }
                }
                else
                {
                    MessageBox.Show("You must select a product!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Customer> customers = customerRepository.FindByName(txtSearchCustomer.Text);
                dgData.ItemsSource = customers;
                txtSearchCustomer.Text = string.Empty;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on search customers");
            }
        }

        private void btnLoadCustomer_Click(object sender, RoutedEventArgs e)
        {
            LoadCustomerList();
        }

        private void btnLogout1_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            this.Close();
            adminWindow.Show();
        }
    }
}
