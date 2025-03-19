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
namespace QlKhachSan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isPanelOpen = false; // Trạng thái bảng
        public MainWindow()
        {
            InitializeComponent();
        }
        /*private void btnDatPhong_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Khách hàng {txtCustomer.Text} đã đặt phòng {txtRoom.Text} thành công!",
                "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }*/




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

        // Xử lý các nút trong bảng tài khoản
        private void ViewAccount_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Xem thông tin tài khoản");
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Đổi mật khẩu");
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Đăng xuất");
        }
    }
}