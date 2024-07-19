using BusinessObject;
using Repositories;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace NAMDNHE176906WPF
{
    /// <summary>
    /// Interaction logic for BookingWindow.xaml
    /// </summary>
    public partial class BookingWindow : Window
    {

        private IBookingReservationRepository bookingReservationRepository;
        public BookingWindow()
        {
            InitializeComponent();
            bookingReservationRepository = new BookingReservationRepository();
        }

        public Customer Account { get; set; }

        private void LoadBooking()
        {
            try
            {
                var bookings = bookingReservationRepository.GetByCustomerID(Account.CustomerID);
                dgBookingHistory.ItemsSource = bookings;
            }
            catch (Exception ex)
            {

            }
        }



        private void btnLogout1_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow customerWindow = new CustomerWindow()
            {
                Account = Account
            };
            this.Close();
            customerWindow.Show();
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var booking = button.DataContext as BookingReservation;
            if (booking != null)
            {
                // Show the booking detail pop-up window passing the booking reservation ID
                BookingDetailWindow detailWindow = new BookingDetailWindow
                {
                    BookingID = booking.BookingReservationID,
                    Account = Account,
                    AdminOrCustomer = false
                };
                this.Close();
                detailWindow.ShowDialog();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadBooking();
        }
    }
}
