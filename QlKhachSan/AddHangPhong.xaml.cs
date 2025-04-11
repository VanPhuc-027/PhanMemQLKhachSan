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
    public partial class AddHangPhong : Window
    {
        public RoomType NewHangPhong { get; private set; }

        public AddHangPhong()
        {
            InitializeComponent();

        }

        public AddHangPhong(RoomType roomType) : this()
        {
            txtTenLoai.Text = roomType.TypeName;
            txtMoTa.Text = roomType.Description;
            txtGiaNgay.Text = roomType.PriceDay.ToString();
            txtGiaGio.Text = roomType.PriceHour.ToString();

            NewHangPhong = new RoomType { RoomTypeId = roomType.RoomTypeId };
        }

        private void BtnThem_Click(object sender, RoutedEventArgs e)
        {
            

            if (!double.TryParse(txtGiaNgay.Text.Trim(), out double giaNgay) || giaNgay < 0)
            {
                MessageBox.Show("Giá theo ngày không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!double.TryParse(txtGiaGio.Text.Trim(), out double giaGio) || giaGio < 0)
            {
                MessageBox.Show("Giá theo giờ không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NewHangPhong = new RoomType
            {
                //RoomTypeId = int.Parse(txtMaLoai.Text.Trim()),
                TypeName = txtTenLoai.Text.Trim(),
                Description = string.IsNullOrWhiteSpace(txtMoTa.Text) ? "Không có mô tả" : txtMoTa.Text.Trim(),
                PriceDay = decimal.Parse(txtGiaNgay.Text.Trim()),
                PriceHour = decimal.Parse(txtGiaGio.Text.Trim())
            };

            this.DialogResult = true;
            this.Close();
        }
    }
}