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
using QlKhachSan.Models;

namespace QlKhachSan
{
    /// <summary>
    /// Interaction logic for DangNhap.xaml
    /// </summary>
    public partial class DangNhap : Window
    {
        public DangNhap()
        {
            InitializeComponent();
            CreateDefaultAdminAccount();
        }

        private void CreateDefaultAdminAccount()
        {
            using (var context = new QlksContext())
            {
                var adminUser = context.Users.SingleOrDefault(u => u.Username == "admin");
                if (adminUser == null)
                {
                    // Nếu tài khoản không tồn tại, tạo mới
                    var user = new User
                    {
                        Username = "admin",
                        Password = "123",
                        Email = "thonghoaiquy@gmail.com",
                        Phone = "0528486653",
                        CreatedAt = DateTime.Now
                    };

                    context.Users.Add(user);
                }
                else
                {
                    // Nếu đã tồn tại, cập nhật thông tin email và số điện thoại
                    adminUser.Email = "thonghoaiquy@gmail.com"; // Cập nhật email mới
                    adminUser.Phone = "0528486653"; // Cập nhật số điện thoại mới
                }

                // Lưu thay đổi vào database
                context.SaveChanges();
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            // Kiểm tra thông tin đăng nhập từ cơ sở dữ liệu
            using (var context = new QlksContext())
            {
                var user = context.Users.SingleOrDefault(u => u.Username == username && u.Password == password);
                if (user != null)
                {
                    // Mở MainWindow
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();

                    // Đóng cửa sổ đăng nhập
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ForgotPassword_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Tính năng quên mật khẩu đang được phát triển!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
