using BusinessObject;
using Microsoft.Extensions.Configuration;
using Repositories;
using System;
using System.Collections.Generic;
using System.IO;
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
using Path = System.IO.Path;

namespace DangTMHE181967WPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        ICustomerRepository customerRepository;
        public LoginWindow()
        {
            InitializeComponent();
            customerRepository = new CustomerRepository();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            if(string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPass.Password))
            {
                MessageBox.Show("Please input all fields", "Login");
                return;
            }

            Customer customer = customerRepository.Login(txtEmail.Text, txtPass.Password);
            if(isValidLogin(txtEmail.Text, txtPass.Password))
            {
                AdminWindow adminWindow = new AdminWindow();
                adminWindow.Show();
                this.Close();
            } else if (customer != null)
            {
                CustomerWindow customerWindow = new CustomerWindow
                {
                    Account = customer
                };
                customerWindow.Show();
                this.Close();
            } else
            {
                MessageBox.Show("Login failed! Email or password incorrect", "Login");
            }
        }

        private bool isValidLogin(string email, string password)
        {
            IConfiguration config = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json", true, true)
               .Build();
            var adminEmail = config["Admin:Email"];
            var adminPass = config["Admin:Password"];
            if (adminEmail == email && adminPass == password)
            {
                return true;
            }
            return false;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
