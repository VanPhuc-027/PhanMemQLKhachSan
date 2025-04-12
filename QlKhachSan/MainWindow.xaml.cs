using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using MaterialDesignThemes.Wpf;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QlKhachSan.Models;
using LiveCharts;
using LiveCharts.Wpf;
using System.Globalization;

namespace QlKhachSan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isPanelOpen = false; // Trạng thái bảng
        public List<string> Labels { get; set; } = new();
        public SeriesCollection SeriesCollection { get; set; } = new();
        public Func<double, string> Formatter { get; set; }
        private readonly QlksContext _context = new QlksContext();


        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadOccupiedRooms();
            LoadRevenueChart();

        }

        private void btnAccount_Click(object sender, RoutedEventArgs e)
        {
            double targetX = isPanelOpen ? 300 : 0; // Nếu mở thì trượt ra, nếu đóng thì trượt vào

            var animation = new DoubleAnimation
            {
                To = targetX,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseOut }
            };

            
            isPanelOpen = !isPanelOpen;
        }

        // Xử lý các nút trong bảng quản lý
        private void OpenInventory_Click(object sender, RoutedEventArgs e)
        {
            QLHangHoa inventoryWindow = new QLHangHoa();
            inventoryWindow.Show();
        }

        private void OpenEmployee_Click(object sender, RoutedEventArgs e)
        {
            QLNhanVien employeeWindow = new QLNhanVien();
            employeeWindow.Show();
        }

        private void OpenRoom_Click(object sender, RoutedEventArgs e)
        {
            QLPhong roomWindow = new QLPhong();
            roomWindow.Show();
            this.Close();
        }

        // Xử lý các nút trong bảng tài khoản
        private void ViewAccount_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Xem thông tin tài khoản");
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            int userId = 1; // Thay bằng ID thực tế của người dùng đang đăng nhập
            ChangePassword changePasswordWindow = new ChangePassword(userId);
            changePasswordWindow.ShowDialog();
        }


        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Đăng xuất thành công");
            DangNhap dangNhapWindow = new DangNhap();
            dangNhapWindow.Show();
            this.Close();
        }


        private void Booking_Click(object sender, RoutedEventArgs e)
        {
            BookingWindow bookingWindow = new BookingWindow();
            bookingWindow.Show();
            this.Close();

        }

        private void Statistical_Click(object sender, RoutedEventArgs e)
        {
            ThongKe statisticalWindow = new ThongKe();
            statisticalWindow.Show();
            this.Close();
        }

        //Đổ dữ liệu
        private void LoadOccupiedRooms()
        {
            using (var db = new QlksContext())
            {
                var occupiedRooms = db.Rooms
                                    .Include(r => r.RoomType)
                                    .ToList()
                                    .Where(r => r.Status?.Trim().ToLower() == "đã đặt")
                                    .ToList();


                wpPhongDangCoKhach.Children.Clear(); // Xóa dữ liệu cũ

                foreach (var room in occupiedRooms)
                {
                    var border = new Border
                    {
                        BorderBrush = Brushes.DarkRed,
                        BorderThickness = new Thickness(1),
                        CornerRadius = new CornerRadius(8),
                        Padding = new Thickness(8),
                        Margin = new Thickness(5),
                        Width = 180,
                        Background = Brushes.LightYellow,
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
                        Text = $"Loại phòng: {room.RoomType?.TypeName ?? "Không rõ"}",
                        TextAlignment = TextAlignment.Center,
                        FontSize = 12,
                        Margin = new Thickness(0, 0, 0, 5)
                    };

                    panel.Children.Add(txtRoomNumber);
                    panel.Children.Add(txtRoomType);
                    border.Child = panel;

                    wpPhongDangCoKhach.Children.Add(border);
                }
            }
        }
        private void LoadRevenueChart()
        {
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            var payments = _context.Payments
                .Where(p => p.PaymentDate.HasValue &&
                            p.PaymentDate.Value.Year == currentYear &&
                            p.PaymentDate.Value.Month == currentMonth)
                .ToList();

            var grouped = payments
                .GroupBy(p => p.PaymentDate!.Value.Day)
                .Select(g => new
                {
                    Day = g.Key,
                    Total = g.Sum(p => p.TotalAmount)
                })
                .OrderBy(g => g.Day)
                .ToList();

            if (grouped.Count == 0)
            {
                RevenueChartMain.Series = new SeriesCollection();
                RevenueChartMain.AxisX[0].Labels = new List<string>();
                tbThongBaoMain.Visibility = Visibility.Visible;
                return;
            }

            Labels = grouped.Select(g => $"Ngày {g.Day}").ToList();
            var values = grouped.Select(g => (double)g.Total).ToList();

            var culture = CultureInfo.GetCultureInfo("vi-VN");

            SeriesCollection = new SeriesCollection
    {
        new ColumnSeries
        {
            Title = $"Tháng {currentMonth}/{currentYear}",
            Values = new ChartValues<double>(values),
            DataLabels = true,
            LabelPoint = point =>
            {
                int index = (int)point.X;
                string dayLabel = Labels[index];
                double amount = point.Y;

                string formattedAmount = string.Format(culture, "{0:#,0} VNĐ", amount);
                return $"{dayLabel}\n{formattedAmount}";
            }
        }
    };

            Formatter = value => string.Format(culture, "{0:#,0}", value);

            DataContext = this;

            RevenueChartMain.Series = SeriesCollection;
            RevenueChartMain.AxisX[0].Labels = Labels;
            tbThongBaoMain.Visibility = Visibility.Collapsed;
        }


    }

}