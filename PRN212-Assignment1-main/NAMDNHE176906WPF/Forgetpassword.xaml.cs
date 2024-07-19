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
    /// Interaction logic for Forgetpassword.xaml
    /// </summary>
    public partial class Forgetpassword : Window
    {
        public Customer Account { get; set; }
        public Forgetpassword()
        {
            InitializeComponent();
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text.Trim();

            // Validate email format
            if (string.IsNullOrEmpty(email) )
            {
                
                MessageBox.Show("Please enter a valid email address.");
                return;
            }

            // Try to send password reset email
            CustomerDAO dal = new CustomerDAO();
            var list = dal.GetCustomersList();
            int k = 0;
            foreach (var item in list)
            {
                if (item.EmailAddress == email) {
                    
                    Account = item;
                    k++;
                    break;
                }
            }
            if (k == 0) {
                MessageBox.Show("The email was not correct");
                return;
            }
            else
            {
                ResetPassword resetPassword = new ResetPassword();

                MessageBox.Show("The email was correct");
                resetPassword.Account = Account;
                this.Close();
                resetPassword.Show();
            }
            
           
           



        }
    }
}
