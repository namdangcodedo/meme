using BusinessObject;
using DataAccess;
using Microsoft.Data.SqlClient;
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

namespace NAMDNHE176906WPF
{
    /// <summary>
    /// Interaction logic for ConfirmWindow.xaml
    /// </summary>
    public partial class ConfirmWindow : Window
    {

        public ConfirmWindow()
        {
            InitializeComponent();
            LoadBooking();
        }
        private void LoadBooking()
        {
            try
            {
                BookingReservationDAO dal = new BookingReservationDAO();
                var bookings = dal.List();
                lvbooking.ItemsSource = bookings ;
            }
            catch (Exception ex)
            {

            }
        }
        public void btn_confirm(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var booking = button.DataContext as ViewBookingForAdmin;
            if (booking != null)  // Thêm kiểm tra button.Tag không phải null
            {
                
                BookingReservationDAO dal = new BookingReservationDAO();
                dal.UpdateBookingStatus(booking.bookingReservationID);
                MessageBox.Show("Booking Confirmed");
                LoadBooking();
                
            
            }
            else
            {
                MessageBox.Show("Button or Tag is null");
            }
        }




        




    }
}
