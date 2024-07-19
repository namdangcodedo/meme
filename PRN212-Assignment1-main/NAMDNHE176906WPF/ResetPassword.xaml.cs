using BusinessObject;
using DataAccess;
using Microsoft.Identity.Client.NativeInterop;
using System;
using System.Collections.Generic;
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

namespace NAMDNHE176906WPF
{
    /// <summary>
    /// Interaction logic for ResetPassword.xaml
    /// </summary>
    public partial class ResetPassword : Window
    {

        public Customer Account { get; set; }
        public ResetPassword()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string newPassword = txtNewPassword.Password;
            string confirmPassword = txtConfirmPassword.Password;

            // Validate passwords
            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                
                MessageBox.Show("Both fields are required.");
                return;
               
            }

            if (newPassword != confirmPassword)
            {
                
                MessageBox.Show("Passwords do not match.");
                return;
                
            }
            CustomerDAO dal =  new CustomerDAO();
           var check =  dal.SetPassword(Account, newPassword);
            if (check)
            {
                MessageBox.Show("chagne pass successfully");
                this.Hide();
                LoginWindow re = new LoginWindow();
                re.Show();
            }
            else
            {
                MessageBox.Show("Can not  created new pass!");
            }
        }
    }
}
