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
        public Customer Account { get; set; }
       
    public AddBookingWindow()
        {
            InitializeComponent();
        }

       

        private void btnLoadReport_Click(object sender, RoutedEventArgs e)
        {
            dgRooms.ItemsSource = null;
            var startDate = dpStart.Text;
            var endDate = dpEnd.Text;
            RoomInformationDAO dal = new RoomInformationDAO();
            var list = new List<ViewBookingForC>();
            list = dal.GetRoomInformationListC();
           dgRooms.ItemsSource = list;

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
            AddBookingWindow addBookingWindow = new AddBookingWindow();
            this.Close();
            addBookingWindow.Show();
        }

        private void btnadd_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra nếu phòng được chọn
            if (dgRooms.SelectedItem == null)
            {
                MessageBox.Show("Please select a room from the list.");
                return;
            }

            // Kiểm tra nếu ngày bắt đầu hoặc ngày kết thúc chưa được chọn
            if (dpStart.SelectedDate == null || dpEnd.SelectedDate == null)
            {
                MessageBox.Show("Please select both start and end dates.");
                return;
            }

            var roomInformation = dgRooms.SelectedItem as ViewBookingForC;
            DateTime start = dpStart.SelectedDate.Value;
            DateTime end = dpEnd.SelectedDate.Value;
            DateTime today = DateTime.Today;

            // Kiểm tra nếu ngày bắt đầu trước ngày hiện tại
            if (start < today)
            {
                MessageBox.Show("Start date must be today or in the future.");
                return;
            }

            // Kiểm tra nếu ngày kết thúc trước ngày bắt đầu
            if (end <= start)
            {
                MessageBox.Show("End date must be after the start date.");
                return;
            }

            int numberOfDays = (end - start).Days;

            // Tính tổng giá
            decimal totalPrice = numberOfDays * roomInformation.RoomPricePerDate;

            // Kiểm tra nếu Account là null
            if (Account == null)
            {
                MessageBox.Show("User account is not logged in.");
                return;
            }

            // Lấy ID người dùng (giả sử Account là một đối tượng chứa thông tin người dùng hiện tại)
            int userId = Account.CustomerID;

            // Tạo đối tượng BookingReservation và thiết lập các thuộc tính
            BookingReservation booking = new BookingReservation
            {
                TotalPrice = totalPrice,
                BookingDate = start,
                CustomerID = userId,
                BookingStatus = 0
            };

            // Tạo đối tượng DAO và thêm đặt phòng vào cơ sở dữ liệu
            BookingReservationDAO dal = new BookingReservationDAO();
            bool isBookingSuccessful = dal.Add(booking);

            // Kiểm tra kết quả của việc thêm đặt phòng
            if (isBookingSuccessful)
            {
                AddBookingWindow addBookingWindow = new AddBookingWindow();
                this.Close();
                addBookingWindow.Show();
            }
            else
            {
                MessageBox.Show("Cannot book because someone already booked!");
            }
        }




    }
}
