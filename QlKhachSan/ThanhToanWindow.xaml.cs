using Microsoft.EntityFrameworkCore;
using QlKhachSan.Models;
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
using Microsoft.EntityFrameworkCore;


namespace QlKhachSan
{
    /// <summary>
    /// Interaction logic for ThanhToanWindow.xaml
    /// </summary>
    public partial class ThanhToanWindow : Window
    {
        private Booking booking;


        public ThanhToanWindow(Booking selectedBooking)
        {
            InitializeComponent();
            booking = selectedBooking;
            LoadThongTin();
        }


        private void LoadThongTin()
        {
            using var db = new QlksContext();
            var datPhong = db.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Room)
                    .ThenInclude(r => r.RoomType)
                .Include(b => b.BookingServices)
                    .ThenInclude(bs => bs.Service)
                .FirstOrDefault(b => b.BookingId == booking.BookingId);

            if (datPhong != null)
            {
                txtKhachHang.Text = datPhong.Customer.FullName;
                txtEmail.Text = datPhong.Customer.Email;
                txtSDT.Text = datPhong.Customer.Phone;
                txtThoiGian.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                var tienPhong = datPhong.Room.RoomType.PriceDay;
                var tienDichVu = datPhong.BookingServices.Sum(bs => bs.Service.ServicePrice);

                txtTienPhong.Text = tienPhong.ToString("N0") + " VNĐ";
                txtTienDichVu.Text = tienDichVu.ToString("N0") + " VNĐ";
                txtTongTien.Text = (tienPhong + tienDichVu).ToString("N0") + " VNĐ";
            }
        }


        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnXacNhan_Click(object sender, RoutedEventArgs e)
        {
            using var db = new QlksContext();

            var dbBooking = db.Bookings
                .Include(b => b.Room)
                .ThenInclude(r => r.RoomType)
                .FirstOrDefault(b => b.BookingId == booking.BookingId);

            if (dbBooking != null)
            {
                var room = dbBooking.Room;
                if (room != null)
                {
                    room.Status = "Trống";
                }

                // Tính tổng tiền
                var tienPhong = dbBooking.Room.RoomType.PriceDay;
                var tienDichVu = db.BookingServices
                    .Include(bs => bs.Service)
                    .Where(bs => bs.BookingId == dbBooking.BookingId)
                    .Sum(bs => bs.Service.ServicePrice);

                var tongTien = tienPhong + tienDichVu;

                // Lưu hóa đơn vào bảng Payment
                var payment = new Payment
                {
                    BookingId = dbBooking.BookingId,
                    TotalAmount = tongTien,
                    PaymentDate = DateTime.Now,
                    PaymentMethod = (cbPhuongThuc.SelectedItem as ComboBoxItem)?.Content.ToString()
                };
                db.Payments.Add(payment);

                // Cập nhật trạng thái booking nếu muốn
                dbBooking.Status = "Đã trả";

                db.SaveChanges();

                MessageBox.Show("Thanh toán thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
        }

    }

}
