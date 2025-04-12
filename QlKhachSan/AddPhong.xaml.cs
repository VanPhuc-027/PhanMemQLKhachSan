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
using static QlKhachSan.QLPhong;

namespace QlKhachSan
{
    /// <summary>
    /// Interaction logic for AddPhong.xaml
    /// </summary>
    public partial class AddPhong : Window
    {
        private RoomDisplay editingRoom;

        public new Room Newroom { get; set; } // Đối tượng Room mới

        public bool IsAdded { get; private set; }
        public AddPhong()
        {
            InitializeComponent();
            Newroom = new Room();
            DataContext = Newroom;
            LoadRoomTypes();
        }

        private void LoadRoomTypes()
        {
            using (var db = new QlksContext())
            {
                var users = db.RoomTypes.ToList();
                cbxHangPhong.ItemsSource = users;
                cbxHangPhong.DisplayMemberPath = "TypeName"; // Tên hiển thị trong ComboBox
                cbxHangPhong.SelectedValuePath = "RoomTypeId"; // Giá trị thực tế của từng mục
            }
        }
        private void BtnThem_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSoPhong.Text))
            {
                MessageBox.Show("Vui lòng nhập số phòng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (cbxHangPhong.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn hạng phòng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Newroom = new Room
            {
                RoomNumber = txtSoPhong.Text.Trim(),
                RoomTypeId = (int)cbxHangPhong.SelectedValue,
                Status = "Trống"
            };

            using (var db = new QlksContext())
            {
                db.Rooms.Add(Newroom);
                db.SaveChanges();
            }

            IsAdded = true; // Đánh dấu là đã thêm thành công
            MessageBox.Show("Đã thêm phòng mới thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = true; // Đóng form và báo hiệu thành công về form gọi
            this.Close();
        }
    }
}
