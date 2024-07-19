using BusinessObject;
using DataAccess;
using Microsoft.Identity.Client.NativeInterop;
using NAMDNHE176906WPF;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace NAMDNHE176906WPF
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }
        public void SetData(string password, string confirm, string name, string email, string phone, DateTime birth)
        {
            PasswordBox.Password = password;
            ConfirmPasswordBox.Password = confirm;
            FullNameTextBox.Text = name;
            EmailTextBox.Text = email;
            PhoneTextBox.Text = phone;
            BirthdayDatePicker.SelectedDate = birth;
        }

        public  void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy dữ liệu từ các điều khiển
          
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;
            string fullName = FullNameTextBox.Text;
            string email = EmailTextBox.Text;
            string phone = PhoneTextBox.Text;
            DateTime? birthday = BirthdayDatePicker.SelectedDate;

            // Kiểm tra các trường không được để trống
            if (
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword) ||
                string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phone) ||
                birthday == null)
            {
                MessageBox.Show("All fields are required.", "Validation Error");
                return;
            }

            // Kiểm tra mật khẩu nhập lại có khớp với mật khẩu không
            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Validation Error");
                return;
            }
            if (birthday.HasValue)
            {
                int age = DateTime.Now.Year - birthday.Value.Year;
                if (birthday.Value.AddYears(age) > DateTime.Now)
                {
                    age--;
                }

                if (age < 18)
                {
                    MessageBox.Show("You must be at least 18 years old to register.");
                    return;
                }
            }

            // Kiểm tra định dạng email
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Invalid email format.", "Validation Error");
                return;
            }

            // Kiểm tra định dạng số điện thoại (ví dụ: chỉ chứa số và có độ dài từ 10 đến 15 ký tự)
            if (!Regex.IsMatch(phone, @"^\d{10,11}$"))
            {
                MessageBox.Show("Invalid phone number format.", "Validation Error");
                return;
            }

            // Thực hiện các bước đăng ký ở đây
            // Ví dụ: kiểm tra dữ liệu đầu vào, lưu thông tin vào cơ sở dữ liệu, v.v.
            CustomerDAO dal = new CustomerDAO();

            Customer c = new Customer();
            c.CustomerFullName = fullName;
            c.CustomerStatus = 1;
            c.CustomerBirthday = birthday.Value;
            c.EmailAddress = email;
            c.Password = password;
            c.Telephone = phone;


            var list = dal.GetCustomersList();
            var k = 0;
            foreach (var item in list)
            {
                if (item.EmailAddress.Equals(c.EmailAddress))
                {
                    k++;
                }
            }
            var check = false;
            if (k == 0) {
               check = dal.Add(c);

            }
            else
            {
                MessageBox.Show("gmail haved");
                return;
            }
           
            if (check)
            {
                MessageBox.Show("Account created successfully!");
                this.Hide();
                LoginWindow re = new LoginWindow();
                re.Show();
            }
            else
            {
                MessageBox.Show("Cannot create account!");
            }
        }

        // Hàm kiểm tra định dạng email hợp lệ
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        
    }
}
