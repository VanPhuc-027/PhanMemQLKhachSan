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
    public partial class QLHangHoa : Window
    {
        private List<Service> services;

        public QLHangHoa()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (var db = new QlksContext())
            {
                services = db.Services.ToList(); // Lưu danh sách gốc
                dgInventory.ItemsSource = services;
            }
        }


        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddHangHoa();
            if (addWindow.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgInventory.SelectedItem is Service selectedService)
            {
                var editForm = new EditHangHoa(new Service
                {
                    ServiceId = selectedService.ServiceId,
                    ServiceName = selectedService.ServiceName,
                    ServicePrice = selectedService.ServicePrice,
                    Description = selectedService.Description
                });

                if (editForm.ShowDialog() == true)
                {
                    LoadData(); // Cập nhật lại danh sách
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng hóa để chỉnh sửa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgInventory.SelectedItem is Service selectedService &&
                MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                using (var db = new QlksContext())
                {
                    db.Services.Remove(db.Services.Find(selectedService.ServiceId));
                    db.SaveChanges();
                }
                LoadData();
            }
        }


        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(keyword))
            {
                dgInventory.ItemsSource = services; // Hiển thị lại toàn bộ danh sách gốc
            }
            else
            {
                var filteredServices = services
                    .Where(s => s.ServiceName.ToLower().Contains(keyword) || s.ServiceId.ToString().Contains(keyword))
                    .ToList();

                if (filteredServices.Count == 0)
                {
                    
                    dgInventory.ItemsSource = services; // Hiển thị lại toàn bộ danh sách gốc
                }
                else
                {
                    dgInventory.ItemsSource = filteredServices;
                }
            }
        }

        private void TxtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "Tìm kiếm...")
            {
                txtSearch.Text = "";
            }
        }

        private void TxtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Tìm kiếm...";
            }
        }
    }
}
