using LiveCharts.Wpf;
using QlKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class QLHangPhong : Window
    {
        private ObservableCollection<RoomType> dsHangPhong;

        public QLHangPhong()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (var db = new QlksContext())
            {
                var list = db.RoomTypes.ToList(); 
                dsHangPhong = new ObservableCollection<RoomType>(list);
                dgHangPhong.ItemsSource = dsHangPhong;
            }
        }

        private void BtnThem_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddHangPhong(); // form nhập thông tin RoomType
            if (addWindow.ShowDialog() == true)
            {
                var newRoomType = addWindow.NewHangPhong;

                using (var db = new QlksContext())
                {
                    db.RoomTypes.Add(newRoomType);
                    db.SaveChanges();
                }

                dsHangPhong.Add(newRoomType);
                MessageBox.Show("Đã thêm hạng phòng!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnSua_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgHangPhong.SelectedItem as RoomType;
            if (selected != null)
            {
                var editWindow = new AddHangPhong(selected); // truyền RoomType để sửa
                if (editWindow.ShowDialog() == true)
                {
                    using (var db = new QlksContext())
                    {
                        var item = db.RoomTypes.FirstOrDefault(x => x.RoomTypeId == selected.RoomTypeId);
                        if (item != null)
                        {
                            item.TypeName = editWindow.NewHangPhong.TypeName;
                            item.Description = editWindow.NewHangPhong.Description;
                            item.PriceDay = editWindow.NewHangPhong.PriceDay;
                            item.PriceHour = editWindow.NewHangPhong.PriceHour;
                            db.SaveChanges();
                        }
                    }

                    LoadData(); // reload lại để cập nhật giao diện
                }
            }
        }

        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgHangPhong.SelectedItem as RoomType;
            if (selected != null && MessageBox.Show($"Xóa {selected.TypeName}?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                using (var db = new QlksContext())
                {
                    var item = db.RoomTypes.FirstOrDefault(x => x.RoomTypeId == selected.RoomTypeId);
                    if (item != null)
                    {
                        db.RoomTypes.Remove(item);
                        db.SaveChanges();
                    }
                }

                dsHangPhong.Remove(selected); // rồi mới xóa khỏi danh sách hiển thị
            }
        }
    }


    
}