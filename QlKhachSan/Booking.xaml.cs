using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ControlzEx.Standard;
using Microsoft.EntityFrameworkCore;
using QlKhachSan.Models;

namespace QlKhachSan
{
    public partial class BookingWindow : Window
    {
        public BookingWindow()
        {
            InitializeComponent();
            LoadRooms();
        }

        private void LoadRooms(string searchText = "")
        {
            using (var db = new QlksContext())
            {
                var roomsQuery = db.Rooms.Include(r => r.RoomType).AsQueryable();

                // Lọc theo số phòng nếu có từ khóa tìm kiếm
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    roomsQuery = roomsQuery.Where(r => r.RoomNumber.Contains(searchText));
                }

                var rooms = roomsQuery.ToList();
                wpRooms.Children.Clear();

                foreach (var room in rooms)
                {
                    var border = new Border
                    {
                        BorderBrush = Brushes.Gray,
                        BorderThickness = new Thickness(1),
                        CornerRadius = new CornerRadius(8),
                        Padding = new Thickness(5),
                        Margin = new Thickness(5),
                        Width = 180,
                        Background = room.Status == "Trống" ? Brushes.LightGreen :
                                     room.Status == "Đã đặt" ? Brushes.LightYellow :
                                     Brushes.LightGray,
                        Tag = room
                    };

                    var panel = new StackPanel();

                    var txtRoomNumber = new TextBlock
                    {
                        Text = $"Số phòng: {room.RoomNumber}",
                        TextAlignment = TextAlignment.Center,
                        FontWeight = FontWeights.Bold,
                        FontSize = 14
                    };

                    var txtRoomType = new TextBlock
                    {
                        Text = $"Loại phòng: {room.RoomType.TypeName}",
                        TextAlignment = TextAlignment.Center,
                        FontSize = 12,
                        Margin = new Thickness(0, 0, 0, 5)
                    };

                    panel.Children.Add(txtRoomNumber);
                    panel.Children.Add(txtRoomType);

                    if (room.Status == "Đã đặt")
                    {
                        var btnEdit = new Button
                        {
                            Content = "Điều chỉnh",
                            Margin = new Thickness(0, 0, 0, 5),
                            Tag = room
                        };
                        btnEdit.Click += (s, e) =>
                        {
                            using (var db = new QlksContext())
                            {
                                var latestBooking = db.Bookings
                                    .Include(b => b.Customer)
                                    .Include(b => b.Room)
                                        .ThenInclude(r => r.RoomType)
                                    .Include(b => b.BookingServices)
                                        .ThenInclude(bs => bs.Service)
                                    .Where(b => b.RoomId == room.RoomId && b.Status != "Đã trả")
                                    .OrderByDescending(b => b.BookingDate)
                                    .FirstOrDefault();

                                if (latestBooking != null)
                                {
                                    var editWindow = new EditBookingWindow(latestBooking);
                                    bool? result = editWindow.ShowDialog();
                                    if (result == true)
                                    {
                                        LoadRooms();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Không tìm thấy booking để chỉnh sửa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                            }
                        };
                        panel.Children.Add(btnEdit);

                        var btnCheckout = new Button
                        {
                            Content = "Trả phòng",
                            Tag = room
                        };
                        btnCheckout.Click += (s, e) =>
                        {
                            using (var db = new QlksContext())
                            {
                                var latestBooking = db.Bookings
                                    .Include(b => b.Customer)
                                    .Include(b => b.Room)
                                        .ThenInclude(r => r.RoomType)
                                    .Include(b => b.BookingServices)
                                        .ThenInclude(bs => bs.Service)
                                    .Where(b => b.RoomId == room.RoomId && b.Status != "Đã trả")
                                    .OrderByDescending(b => b.BookingDate)
                                    .FirstOrDefault();

                                if (latestBooking != null)
                                {
                                    var thanhToan = new ThanhToanWindow(latestBooking);
                                    bool? result = thanhToan.ShowDialog();
                                    if (result == true)
                                    {
                                        LoadRooms();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Không tìm thấy đơn đặt phòng để thanh toán!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                            }
                        };
                        panel.Children.Add(btnCheckout);
                    }
                    else if (room.Status == "Trống")
                    {
                        border.MouseLeftButtonUp += (s, e) =>
                        {
                            var datPhongWindow = new DatPhongChiTiet(room);
                            bool? result = datPhongWindow.ShowDialog();
                            if (result == true)
                                LoadRooms();
                        };
                    }

                    border.Child = panel;
                    wpRooms.Children.Add(border);
                }
            }
        }


        private void RoomButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var room = btn?.Tag as Room;

            if (room != null && room.Status == "Trống")
            {
                var datPhongWindow = new DatPhongChiTiet(room);
                bool? result = datPhongWindow.ShowDialog();
                datPhongWindow.ShowDialog();
                if (result == true)
                {
                    LoadRooms();
                }
            }
            else
            {
                MessageBox.Show("Phòng đã có khách!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadRooms(txtSearch.Text);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadRooms(txtSearch.Text);
        }
        private void btn_BackToMain_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close(); // Đóng cửa sổ quản lý phòng
        }

    }
}


