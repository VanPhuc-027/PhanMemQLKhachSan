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
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace QlKhachSan
{
    /// <summary>
    /// Interaction logic for ThongKe.xaml
    /// </summary>
    public partial class ThongKe : Window
    {
        public SeriesCollection DichVuData { get; set; }
        public List<string> DichVuLabels { get; set; }

        public SeriesCollection DoanhThuData { get; set; }
        public List<string> DoanhThuLabels { get; set; }

        public Func<double, string> Formatter { get; set; }
        public Func<double, string> FormatterDichVu { get; set; }

        public ThongKe()
        {
            InitializeComponent();
            LoadData(DateTime.MinValue, DateTime.MaxValue);
            Formatter = value => value.ToString("N0") + " đ"; // Định dạng số tiền
            FormatterDichVu = value => value.ToString("N0") + " lượt";
            DataContext = this;
        }
        private void LoadData(DateTime startDate, DateTime endDate)
        {
            var dichVuStats = GetDichVuData(startDate, endDate);
            var doanhThuStats = GetDoanhThuData(startDate, endDate);

            // Biểu đồ số lượng dịch vụ
            DichVuData = new SeriesCollection
    {
        new ColumnSeries
        {
            Title = "Số lượng dịch vụ",
            Values = new ChartValues<int>(dichVuStats.Values),
            Fill = new SolidColorBrush(Colors.Blue),
            Stroke = new SolidColorBrush(Colors.DarkBlue),
            StrokeThickness = 2,
            DataLabels = true,
            FontSize = 14, // Tăng kích thước chữ
            MaxColumnWidth = 50 // Điều chỉnh độ rộng cột
        }
    };
            DichVuLabels = new List<string>(dichVuStats.Keys);

            // Biểu đồ doanh thu
            DoanhThuData = new SeriesCollection
    {
        new ColumnSeries
    {
        Title = "Doanh thu (VNĐ)",
        Values = new ChartValues<double>(doanhThuStats.Values.Select(v => (double)v)),
        Fill = new SolidColorBrush(Colors.OrangeRed),
        Stroke = new SolidColorBrush(Colors.DarkRed),
        StrokeThickness = 2,
        DataLabels = true,
        FontSize = 14,
        MaxColumnWidth = 50,
        LabelPoint = point => string.Format("{0:N0} đ", point.Y) // Định dạng số tiền
    }
    };
            DoanhThuLabels = new List<string>(doanhThuStats.Keys);

            DataContext = this;
        }



        private void BtnFilter_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = dpStartDate.SelectedDate ?? DateTime.MinValue;
            DateTime endDate = dpEndDate.SelectedDate ?? DateTime.MaxValue;
            LoadData(startDate, endDate);
        }

        // Giả lập lấy dữ liệu dịch vụ từ cơ sở dữ liệu
        private Dictionary<string, int> GetDichVuData(DateTime startDate, DateTime endDate)
        {
            Dictionary<string, int> data = new Dictionary<string, int>
            {
                { "Massage", 120 },
                { "Giặt ủi", 80 },
                { "Thuê xe", 50 },
                { "Ăn uống", 150 }
            };

            // Có thể thay thế bằng truy vấn SQL để lấy dữ liệu thực tế từ CSDL theo ngày tháng
            return data;
        }

        // Giả lập lấy dữ liệu doanh thu từ cơ sở dữ liệu
        private Dictionary<string, int> GetDoanhThuData(DateTime startDate, DateTime endDate)
        {
            Dictionary<string, int> data = new Dictionary<string, int>
            {
                { "Tháng 1", 50000 },
                { "Tháng 2", 70000 },
                { "Tháng 3", 65000 },
                { "Tháng 4", 80000 }
            };

            // Có thể thay thế bằng truy vấn SQL để lấy dữ liệu thực tế từ CSDL theo ngày tháng
            return data;
        }
    }
}
