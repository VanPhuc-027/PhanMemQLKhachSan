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
                var rooms = db.Rooms.Include(r => r.RoomType).ToList();
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
                        Width = 120,
                        Background = room.Status == "Trống" ? Brushes.LightGreen :
                                     room.Status == "Đã đặt" ? Brushes.LightYellow :
                                     Brushes.LightGray,
                        Tag = room
                    };

                    var panel = new StackPanel();

                    // Thông tin phòng
                    var txtInfo = new TextBlock
                    {
                        Text = $"{room.RoomNumber}\n{room.RoomType.TypeName}",
                        TextAlignment = TextAlignment.Center,
                        FontWeight = FontWeights.Bold,
                        Margin = new Thickness(0, 0, 0, 5)
                    };
                    panel.Children.Add(txtInfo);

                    if (room.Status == "Đã đặt")
                    {
                        // Nút Điều chỉnh
                        var btnEdit = new Button
                        {
                            Content = "Điều chỉnh",
                            Margin = new Thickness(0, 0, 0, 5),
                            Tag = room
                        };
                        btnEdit.Click += (s, e) =>
                        {
                            var datPhongWindow = new DatPhongChiTiet(room);
                            bool? result = datPhongWindow.ShowDialog();
                            if (result == true)
                                LoadRooms();
                        };
                        panel.Children.Add(btnEdit);

                        // Nút Trả phòng
                        var btnCheckout = new Button
                        {
                            Content = "Trả phòng",
                            Tag = room
                        };
                        btnCheckout.Click += (s, e) =>
                        {
                            using var dbContext = new QlksContext();
                            var dbRoom = dbContext.Rooms.FirstOrDefault(r => r.RoomId == room.RoomId);
                            if (dbRoom != null)
                            {
                                dbRoom.Status = "Trống";
                                dbContext.SaveChanges();
                                MessageBox.Show("Trả phòng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                                LoadRooms();
                            }
                        };
                        panel.Children.Add(btnCheckout);
                    }
                    else if (room.Status == "Trống")
                    {
                        // Gắn sự kiện click để mở form đặt phòng khi phòng trống
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

    }
}


