using Microsoft.EntityFrameworkCore;
using QlKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace QlKhachSan
{
    public partial class EditBookingWindow : Window
    {
        private Booking booking;
        private Room room;
        private List<Service> allServices = new List<Service>();

        public EditBookingWindow(Booking booking)
        {
            InitializeComponent();
            this.booking = booking;
            InitComboBoxes();

            dpCheckInDate.SelectedDateChanged += CapNhatTongTien;
            dpCheckOutDate.SelectedDateChanged += CapNhatTongTien;
            cbCheckInHour.SelectionChanged += CapNhatTongTien;
            cbCheckInMinute.SelectionChanged += CapNhatTongTien;
            cbCheckOutHour.SelectionChanged += CapNhatTongTien;
            cbCheckOutMinute.SelectionChanged += CapNhatTongTien;

            LoadServices();
            LoadForm();
        }

        private void LoadForm()
        {
            using var db = new QlksContext();
            booking = db.Bookings
                        .Include(b => b.Customer)
                        .Include(b => b.Room)
                        .ThenInclude(r => r.RoomType)
                        .Include(b => b.BookingServices)
                        .ThenInclude(bs => bs.Service)
                        .FirstOrDefault(b => b.BookingId == booking.BookingId);

            if (booking == null)
            {
                MessageBox.Show("Không tìm thấy thông tin booking!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }

            room = booking.Room;

            txtRoomNumber.Text = room.RoomNumber;
            txtRoomType.Text = room.RoomType?.TypeName ?? "Không rõ";
            txtPriceDay.Text = room.RoomType?.PriceDay.ToString("N0") ?? "0";
            txtPriceHour.Text = room.RoomType?.PriceHour?.ToString("N0") ?? "Không áp dụng";

            var checkIn = booking.CheckInDate ?? DateTime.Now;
            var checkOut = booking.CheckOutDate ?? DateTime.Now;

            dpCheckInDate.SelectedDate = checkIn.Date;
            cbCheckInHour.SelectedItem = checkIn.Hour.ToString("D2");
            cbCheckInMinute.SelectedItem = (checkIn.Minute - checkIn.Minute % 5).ToString("D2");

            dpCheckOutDate.SelectedDate = checkOut.Date;
            cbCheckOutHour.SelectedItem = checkOut.Hour.ToString("D2");
            cbCheckOutMinute.SelectedItem = (checkOut.Minute - checkOut.Minute % 5).ToString("D2");


            txtCustomerName.Text = booking.Customer.FullName;
            txtCCCD.Text = booking.Customer.IdNumber;
            txtPhone.Text = booking.Customer.Phone;
            txtEmail.Text = booking.Customer.Email;

            // Gán dịch vụ đã chọn
            foreach (CheckBox cb in lbServices.Items)
            {
                if (cb.Tag is Service svc)
                {
                    cb.IsChecked = booking.BookingServices.Any(bs => bs.ServiceId == svc.ServiceId);
                }
            }

            CapNhatTongTien();
        }

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

        private void InitComboBoxes()
        {
            var hours = Enumerable.Range(0, 24).Select(h => h.ToString("D2")).ToList();
            var minutes = Enumerable.Range(0, 60).Where(m => m % 5 == 0).Select(m => m.ToString("D2")).ToList();

            cbCheckInHour.ItemsSource = hours;
            cbCheckOutHour.ItemsSource = hours;

            cbCheckInMinute.ItemsSource = minutes;
            cbCheckOutMinute.ItemsSource = minutes;
        }


        private void CapNhatTongTien(object sender = null, EventArgs e = null)
        {
            try
            {
                if (dpCheckInDate.SelectedDate == null || dpCheckOutDate.SelectedDate == null ||
                    cbCheckInHour.SelectedItem == null || cbCheckInMinute.SelectedItem == null ||
                    cbCheckOutHour.SelectedItem == null || cbCheckOutMinute.SelectedItem == null)
                {
                    txtTotalPrice.Text = "0 VNĐ";
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
                    txtTotalPrice.Text = "0 VNĐ";
                    return;
                }

                TimeSpan duration = checkOut - checkIn;
                var totalHours = duration.TotalHours;
                var totalDays = Math.Ceiling(duration.TotalDays);

                decimal totalPrice = 0;
                if (room.RoomType.PriceHour.HasValue && totalHours < 8)
                {
                    totalPrice = (decimal)Math.Ceiling(totalHours) * room.RoomType.PriceHour.Value;
                }
                else
                {
                    totalPrice = (decimal)totalDays * room.RoomType.PriceDay;
                }

                totalPrice += GetSelectedServiceTotal();
                txtTotalPrice.Text = totalPrice.ToString("N0") + " VNĐ";
            }
            catch
            {
                txtTotalPrice.Text = "0 VNĐ";
            }
        }

        private void btnLuuThayDoi_Click(object sender, RoutedEventArgs e)
        {
            using var db = new QlksContext();
            var updatedBooking = db.Bookings
                .Include(b => b.BookingServices)
                .FirstOrDefault(b => b.BookingId == booking.BookingId);

            if (updatedBooking == null) return;

            // Cập nhật thời gian
            updatedBooking.CheckInDate = dpCheckInDate.SelectedDate.Value
                .AddHours(int.Parse(cbCheckInHour.SelectedItem.ToString()))
                .AddMinutes(int.Parse(cbCheckInMinute.SelectedItem.ToString()));

            updatedBooking.CheckOutDate = dpCheckOutDate.SelectedDate.Value
                .AddHours(int.Parse(cbCheckOutHour.SelectedItem.ToString()))
                .AddMinutes(int.Parse(cbCheckOutMinute.SelectedItem.ToString()));

            // Cập nhật khách hàng
            var customer = db.Customers.FirstOrDefault(c => c.CustomerId == updatedBooking.CustomerId);
            if (customer != null)
            {
                customer.FullName = txtCustomerName.Text.Trim();
                customer.IdNumber = txtCCCD.Text.Trim();
                customer.Phone = txtPhone.Text.Trim();
                customer.Email = txtEmail.Text.Trim();
            }

            // Cập nhật dịch vụ
            db.BookingServices.RemoveRange(updatedBooking.BookingServices);
            foreach (CheckBox item in lbServices.Items)
            {
                if (item.IsChecked == true && item.Tag is Service selectedService)
                {
                    db.BookingServices.Add(new BookingService
                    {
                        BookingId = updatedBooking.BookingId,
                        ServiceId = selectedService.ServiceId
                    });
                }
            }

            db.SaveChanges();
            MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
        }
    }
}
