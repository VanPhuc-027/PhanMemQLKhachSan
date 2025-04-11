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
    public partial class EditHangHoa : Window
    {
        private Service _service;

        public EditHangHoa(Service service)
        {
            InitializeComponent();

            if (service == null)
            {
                MessageBox.Show("Không có dữ liệu hàng hóa!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }

            _service = service;
            DataContext = _service; // Bind dữ liệu
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new QlksContext())
            {
                var existingService = db.Services.Find(_service.ServiceId);
                if (existingService != null)
                {
                    existingService.ServiceName = TxtServiceName.Text;
                    existingService.ServicePrice = decimal.Parse(TxtPrice.Text);
                    existingService.Description = TxtDescription.Text;
                    db.SaveChanges();
                }
            }
            DialogResult = true;
            Close();
        }


        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}