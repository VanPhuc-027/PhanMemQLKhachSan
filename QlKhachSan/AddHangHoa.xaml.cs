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
    public partial class AddHangHoa : Window
    {
        private Service _service;

        public AddHangHoa()
        {
            InitializeComponent();
            _service = new Service();  // Tạo mới hàng hóa
            DataContext = _service;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new QlksContext())
            {
                var newService = new Service
                {
                    ServiceName = TxtServiceName.Text,
                    ServicePrice = decimal.Parse(TxtPrice.Text),
                    Description = TxtDescription.Text
                };

                db.Services.Add(newService);
                db.SaveChanges();
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