using BusinessObject;
using DataAccess;
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
    /// Interaction logic for AddBookingWindow.xaml
    /// </summary>
    public partial class AddBookingWindow : Window
    {
       
    public AddBookingWindow()
        {
            InitializeComponent();
        }

       

        private void btnLoadReport_Click(object sender, RoutedEventArgs e)
        {
            var startDate = dpStart.Text;
            var endDate = dpEnd.Text;
            RoomInformationDAO dal = new RoomInformationDAO();
            var list = new List<ViewBookingForC>();
            list = dal.GetRoomInformationListC();
            ListView.ItemsSource = list;
        }

        private void btnLogout1_Click(object sender, RoutedEventArgs e)
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            this.Close();
            adminWindow.Show();
        }
    }
}
