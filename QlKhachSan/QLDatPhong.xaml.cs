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
    /// Interaction logic for QLDatPhong.xaml
    /// </summary>
    public partial class QLDatPhong : UserControl
    {
        public QLDatPhong()
        {
            InitializeComponent();
        }
        private void ThemDonDatPhong_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Bạn đã nhấn Thêm Đơn Đặt Phòng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
