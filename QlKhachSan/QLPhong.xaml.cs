using System;
using System.Collections.Generic;
using System.Configuration;
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
using LiveCharts.Wpf;
using Microsoft.EntityFrameworkCore;
using QlKhachSan.Models;

namespace QlKhachSan
{
    /// <summary>
    /// Interaction logic for QLPhong.xaml
    /// </summary>
    public partial class QLPhong : Window
    {
        private List<Room> Room;
        public class RoomDisplay
        {
            public string RoomNumber { get; set; }
            public string TypeName { get; set; } 
            public string PriceHour { get; set; } 
            public string PriceDay { get; set; } 
        }

        public QLPhong()
        {
            InitializeComponent();
            LoadRoomList();
        }
        private void LoadRoomList()
        {
            using (var db = new QlksContext())
            {
                var roomList = db.Rooms
                    .Include(r => r.RoomType) // đảm bảo EF load thông tin loại phòng
                    .Select(r => new RoomDisplay
                    {
                        RoomNumber = r.RoomNumber,
                        TypeName = r.RoomType.TypeName,
                        PriceHour = r.RoomType.PriceHour.ToString(),
                        PriceDay = r.RoomType.PriceDay.ToString()
                    })
                    .ToList();

                dgDanhSachPhong.ItemsSource = roomList;
            }
        }
        private void btn_AddRoom(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddPhong();
            if (addWindow.ShowDialog() == true)
            {
                LoadRoomList();
            }
        }
        private void btn_EditRoom(object sender, RoutedEventArgs e)
        {
            if (dgDanhSachPhong.SelectedItem is RoomDisplay selectedRoom)
            {
                var editWindow = new EditPhong(selectedRoom);
                if (editWindow.ShowDialog() == true)
                {
                    LoadRoomList();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một phòng để sửa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btn_DeleteRoom(object sender, RoutedEventArgs e)
        {
            if (dgDanhSachPhong.SelectedItem is RoomDisplay selectedRoom)
            {
                if (MessageBox.Show($"Bạn có chắc muốn xóa phòng {selectedRoom.RoomNumber}?",
                                    "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    using (var db = new QlksContext())
                    {
                        var roomToDelete = db.Rooms.FirstOrDefault(r => r.RoomNumber == selectedRoom.RoomNumber);
                        if (roomToDelete != null)
                        {
                            db.Rooms.Remove(roomToDelete);
                            db.SaveChanges();
                            MessageBox.Show("Đã xóa phòng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            LoadRoomList(); // reload danh sách sau khi xóa
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy phòng để xóa.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một phòng để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void btn_ManageRoomType_Click(object sender, RoutedEventArgs e)
        {
            var qlHangPhongWindow = new QLHangPhong();
            qlHangPhongWindow.ShowDialog();
        }

        private void btn_BackToMain_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close(); // Đóng cửa sổ quản lý phòng
        }
    }
}
