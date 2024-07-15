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

namespace DangTMHE181967WPF
{
    /// <summary>
    /// Interaction logic for ManageRoomWindow.xaml
    /// </summary>
    public partial class ManageRoomWindow : Window
    {

        private IRoomInformationRepository roomInformationRepository;

        public ManageRoomWindow()
        {
            InitializeComponent();
            roomInformationRepository = new RoomInformationRepository();
        }

        private void LoadRoomList()
        {
            try
            {
                var roomList = roomInformationRepository.GetRoomInformationList();
                dgData.ItemsSource = roomList;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error on load list of rooms");
            }
        }

        private void btnLoadRoom_Click(object sender, RoutedEventArgs e)
        {
            LoadRoomList();
        }

        private void btnAddRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RoomDetail roomDetail = new RoomDetail()
                {
                    Title = "Add Room",
                    InsertOrUpdate = false,
                    RoomInformationRepository = roomInformationRepository
                };
                roomDetail.Closed += (s, args) => LoadRoomList(); // Add this line to refresh the customer list after closing
                roomDetail.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdateRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtRoomID.Text.Length > 0)
                {
                    int id = Int32.Parse(txtRoomID.Text);
                    RoomInformation room = roomInformationRepository.GetRoomInformationByID(id);

                    RoomDetail roomDetail = new RoomDetail()
                    {
                        Title = "Update Room",
                        InsertOrUpdate = true,
                        RoomInformation = room,
                        RoomInformationRepository = roomInformationRepository
                    };
                    roomDetail.Closed += (s, args) => LoadRoomList(); // Add this line to refresh the customer list after closing
                    roomDetail.ShowDialog();
                }
                else
                {
                    MessageBox.Show("You must select a room!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtRoomID.Text.Length > 0)
                {
                    int id = int.Parse(txtRoomID.Text);
                    RoomInformation room = roomInformationRepository.GetRoomInformationByID(id);
                    MessageBoxResult result = MessageBox
                        .Show($"Do you want to delete room {room.RoomNumber}?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        roomInformationRepository.RemoveRoomInformation(room);
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

        private void btnSearchRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<RoomInformation> rooms = roomInformationRepository.FindByRoomNumber(txtSearchRoom.Text);
                dgData.ItemsSource = rooms;
                txtSearchRoom.Text = string.Empty;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error on search rooms");
            }
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid? dataGrid = sender as DataGrid;
            DataGridRow? row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dgData.SelectedIndex);
            DataGridCell? rowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
            if (rowColumn != null)
            {
                int id = Int32.Parse(((TextBlock)rowColumn.Content).Text);
                RoomInformation roomInformation = roomInformationRepository.GetRoomInformationByID(id);
                txtSearchRoom.Text = roomInformation.RoomNumber;
                txtRoomID.Text = roomInformation.RoomID.ToString();
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
    }
}
