using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using QlKhachSan.Models;
using static QlKhachSan.QLPhong;

namespace QlKhachSan
{
    public partial class EditPhong : Window
    {
        private string originalRoomNumber;

        public EditPhong(RoomDisplay roomToEdit)
        {
            InitializeComponent();
            originalRoomNumber = roomToEdit.RoomNumber;
            LoadRoomTypes();

            txtRoomNumber.Text = roomToEdit.RoomNumber;
            cmbRoomType.Text = roomToEdit.TypeName;
        }

        private void LoadRoomTypes()
        {
            using (var db = new QlksContext())
            {
                var types = db.RoomTypes.Select(rt => rt.TypeName).ToList();
                cmbRoomType.ItemsSource = types;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new QlksContext())
            {
                var room = db.Rooms.Include(r => r.RoomType)
                                   .FirstOrDefault(r => r.RoomNumber == originalRoomNumber);

                if (room != null)
                {
                    room.RoomNumber = txtRoomNumber.Text;
                    var roomType = db.RoomTypes.FirstOrDefault(rt => rt.TypeName == cmbRoomType.Text);
                    if (roomType != null)
                    {
                        room.RoomTypeId = roomType.RoomTypeId;
                    }

                    db.SaveChanges();
                    MessageBox.Show("Cập nhật phòng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy phòng để cập nhật.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
