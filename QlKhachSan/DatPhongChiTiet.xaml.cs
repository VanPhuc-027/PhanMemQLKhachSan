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

namespace QlKhachSan
{

    /// <summary>
    /// Interaction logic for DatPhongChiTiet.xaml
    /// </summary>
    public partial class DatPhongChiTiet : Window
    {
        private Room room;

        private List<Service> allServices = new List<Service>();

        private void LoadServices()
        {
            using var db = new QlksContext();
            allServices = db.Services.ToList();

            lbServices.Items.Clear();
            foreach (var service in allServices)
            {
                var checkbox = new CheckBox
                {
                    Content = $"{service.ServiceName} - {service.ServicePrice:C0}",
                    Tag = service
                };
                checkbox.Checked += (s, e) => CapNhatTongTien();
                checkbox.Unchecked += (s, e) => CapNhatTongTien();
                lbServices.Items.Add(checkbox);
            }

        }

        private decimal GetSelectedServiceTotal()
        {
            decimal total = 0;
            foreach (CheckBox item in lbServices.Items)
            {
                if (item.IsChecked == true && item.Tag is Service service)
                {
                    total += service.ServicePrice;
                }
            }
            return total;
        }


        public DatPhongChiTiet()
        {
            InitializeComponent();
            dpCheckInDate.SelectedDateChanged += dpCheckInDate_SelectedDateChanged;
            dpCheckOutDate.SelectedDateChanged += dpCheckOutDate_SelectedDateChanged;
            cbCheckInHour.SelectionChanged += cbCheckInHour_SelectionChanged;
            cbCheckInMinute.SelectionChanged += cbCheckInMinute_SelectionChanged;
            cbCheckOutHour.SelectionChanged += cbCheckOutHour_SelectionChanged;
            cbCheckOutMinute.SelectionChanged += cbCheckOutMinute_SelectionChanged;

        }

        public DatPhongChiTiet(Room room)
        {
            InitializeComponent();
            LoadServices();
            this.room = room;
            txtRoomNumber.Text = room.RoomNumber;

            if (room.RoomType != null)
            {
                txtRoomType.Text = room.RoomType.TypeName;
                txtPriceDay.Text = room.RoomType.PriceDay.ToString("N0");
                txtPriceHour.Text = room.RoomType.PriceHour.HasValue
                    ? room.RoomType.PriceHour.Value.ToString("N0")
                    : "Không áp dụng";
            }
            else
            {
                txtRoomType.Text = "Không rõ";
                txtPriceDay.Text = "0";
                txtPriceHour.Text = "Không áp dụng";
                MessageBox.Show("Không thể lấy thông tin loại phòng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            // Khởi tạo giờ và phút
            for (int i = 0; i < 24; i++)
            {
                cbCheckInHour.Items.Add(i.ToString("D2"));
                cbCheckOutHour.Items.Add(i.ToString("D2"));
            }

            for (int i = 0; i < 60; i += 5)
            {
                cbCheckInMinute.Items.Add(i.ToString("D2"));
                cbCheckOutMinute.Items.Add(i.ToString("D2"));
            }

            // Gán mặc định thời gian hiện tại
            var now = DateTime.Now;
            dpCheckInDate.SelectedDate = now.Date;
            cbCheckInHour.SelectedItem = now.Hour.ToString("D2");
            cbCheckInMinute.SelectedItem = (now.Minute - now.Minute % 5).ToString("D2");

            dpCheckOutDate.SelectedDate = now.Date;
            cbCheckOutHour.SelectedItem = now.Hour.ToString("D2");
            cbCheckOutMinute.SelectedItem = (now.Minute - now.Minute % 5 + 30).ToString("D2");
        }

        private void btnDatPhong_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra logic đầu vào
            string idNumber = txtCCCD.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string email = txtEmail.Text.Trim();
            string fullName = txtCustomerName.Text.Trim();

            if (string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("Vui lòng nhập họ tên khách hàng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(idNumber, @"^\d{12}$"))
            {
                MessageBox.Show("CCCD phải gồm đúng 12 chữ số!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(phone, @"^(0|\+84)\d{9}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ! Định dạng: 0xxxxxxxxx hoặc +84xxxxxxxxx", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!string.IsNullOrEmpty(email) && !System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using var db = new QlksContext();
            var room = db.Rooms.FirstOrDefault(r => r.RoomNumber == txtRoomNumber.Text);

            // Lấy thông tin thời gian

            // Kiểm tra ngày giờ nhận và trả phòng đã được chọn chưa
            if (dpCheckOutDate.SelectedDate == null || cbCheckOutHour.SelectedItem == null || cbCheckOutMinute.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ ngày và giờ trả phòng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            DateTime checkIn = dpCheckInDate.SelectedDate.Value
                .AddHours(int.Parse(cbCheckInHour.SelectedItem.ToString()))
                .AddMinutes(int.Parse(cbCheckInMinute.SelectedItem.ToString()));

            DateTime checkOut = dpCheckOutDate.SelectedDate.Value
                .AddHours(int.Parse(cbCheckOutHour.SelectedItem.ToString()))
                .AddMinutes(int.Parse(cbCheckOutMinute.SelectedItem.ToString()));

            if (checkOut <= checkIn)
            {
                MessageBox.Show("Thời gian trả phòng phải sau thời gian nhận phòng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Tính tiền
            TimeSpan duration = checkOut - checkIn;
            decimal totalPrice = 0;

            var totalHours = Math.Ceiling(duration.TotalHours);
            var totalDays = Math.Ceiling(duration.TotalDays);
            var Room = db.Rooms.Include(r => r.RoomType).FirstOrDefault(r => r.RoomNumber == txtRoomNumber.Text);

            if (room.RoomType.PriceHour.HasValue && totalHours < 8)
            {
                totalPrice = (decimal)totalHours * room.RoomType.PriceHour.Value;
            }
            else
            {
                totalPrice = (decimal)totalDays * room.RoomType.PriceDay;
            }


            txtTotalPrice.Text = totalPrice.ToString("N0") + " VNĐ";
            totalPrice += GetSelectedServiceTotal();


            var existingCustomer = db.Customers.FirstOrDefault(c => c.IdNumber == idNumber);
            if (existingCustomer == null)
            {
                existingCustomer = new Customer
                {
                    FullName = fullName,
                    IdNumber = idNumber,
                    Phone = phone,
                    Email = email
                };
                db.Customers.Add(existingCustomer);
                db.SaveChanges();
            }

            var booking = new Booking
            {
                RoomId = room.RoomId,
                CustomerId = existingCustomer.CustomerId,
                CheckInDate = checkIn,
                CheckOutDate = checkOut,
                Status = "Đã đặt"
            };
            db.Bookings.Add(booking);

            // Sau khi booking được thêm, cần lưu lại để có BookingId
            db.SaveChanges();

            // Thêm các dịch vụ đã chọn vào bảng BookingServices
            foreach (CheckBox item in lbServices.Items)
            {
                if (item.IsChecked == true && item.Tag is Service selectedService)
                {
                    var bookingService = new BookingService
                    {
                        BookingId = booking.BookingId,
                        ServiceId = selectedService.ServiceId
                    };
                    db.BookingServices.Add(bookingService);
                }
            }


            room.Status = "Đã đặt";
            db.SaveChanges();

            MessageBox.Show("Đặt phòng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = true;
        }

        private void CapNhatTongTien(object sender = null, EventArgs e = null)
        {
            if (dpCheckInDate.SelectedDate == null || dpCheckOutDate.SelectedDate == null ||
                cbCheckInHour.SelectedItem == null || cbCheckInMinute.SelectedItem == null ||
                cbCheckOutHour.SelectedItem == null || cbCheckOutMinute.SelectedItem == null)
            {
                txtTotalPrice.Text = "0 VND";
                return;
            }

            try
            {
                DateTime checkIn = dpCheckInDate.SelectedDate.Value
                    .AddHours(int.Parse(cbCheckInHour.SelectedItem.ToString()))
                    .AddMinutes(int.Parse(cbCheckInMinute.SelectedItem.ToString()));

                DateTime checkOut = dpCheckOutDate.SelectedDate.Value
                    .AddHours(int.Parse(cbCheckOutHour.SelectedItem.ToString()))
                    .AddMinutes(int.Parse(cbCheckOutMinute.SelectedItem.ToString()));

                if (checkOut <= checkIn)
                {
                    txtTotalPrice.Text = "0 VND";
                    return;
                }

                TimeSpan duration = checkOut - checkIn;
                double totalHours = duration.TotalHours;
                double totalDays = Math.Ceiling(duration.TotalDays);

                decimal totalPrice = 0;

                if (room != null && room.RoomType != null)
                {
                    if (room.RoomType.PriceHour.HasValue && totalHours < 8)
                    {
                        totalPrice = (decimal)Math.Ceiling(totalHours) * room.RoomType.PriceHour.Value;
                    }
                    else
                    {
                        totalPrice = (decimal)totalDays * room.RoomType.PriceDay;
                    }
                }
                decimal serviceTotal = GetSelectedServiceTotal();
                totalPrice += serviceTotal;
                txtTotalPrice.Text = totalPrice.ToString("N0") + " VNĐ";
                
            }
            catch
            {
                txtTotalPrice.Text = "0 VND";
            }

        }


        private void dpCheckInDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CapNhatTongTien();
        }
        private void dpCheckOutDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CapNhatTongTien();
        }
        private void cbCheckInHour_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CapNhatTongTien();
        }
        private void cbCheckInMinute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CapNhatTongTien();
        }
        private void cbCheckOutHour_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CapNhatTongTien();
        }
        private void cbCheckOutMinute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CapNhatTongTien();
        }
    }
}
