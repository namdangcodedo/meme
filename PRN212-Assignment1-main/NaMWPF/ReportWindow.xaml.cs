using BusinessObject;
using Microsoft.Identity.Client.NativeInterop;
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

namespace DangTMHE181967WPF
{
    /// <summary>
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {

        private IBookingReservationRepository bookingReservationRepository;

        public ReportWindow()
        {
            InitializeComponent();
            bookingReservationRepository = new BookingReservationRepository();
        }

        private void btnLoadReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime startDate = dpStart.SelectedDate ?? DateTime.MinValue;
                DateTime endDate = dpEnd.SelectedDate ?? DateTime.MaxValue;
                if(startDate >= endDate)
                {
                    MessageBox.Show("Start date must be smaller than end date", "Generate report");
                }
                var bookings = bookingReservationRepository.GetBookingByDateRange(startDate, endDate);
                dgData.ItemsSource = bookings;
            }
            catch(Exception ex)
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
            AdminWindow adminWindow = new AdminWindow();
            this.Close();
            adminWindow.Show();
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
                    AdminOrCustomer = true
                };
                this.Close();
                detailWindow.ShowDialog();
            }
        }
    }
}
