using LiveCharts;
using LiveCharts.Wpf;
using QlKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace QlKhachSan
{
    public partial class ThongKe : Window
    {
        private readonly QlksContext _context = new QlksContext();

        public List<string> Labels { get; set; } = new();
        public SeriesCollection SeriesCollection { get; set; } = new();

        public ThongKe()
        {
            InitializeComponent();
            InitMonthYearComboBox();
            LoadRevenueByMonth((int)cbNam.SelectedItem, (int)cbThang.SelectedItem);

        }

        private void LoadRevenueByMonth(int year, int month)
        {


            var payments = _context.Payments
                .Where(p => p.PaymentDate.HasValue &&
                            p.PaymentDate.Value.Year == year &&
                            p.PaymentDate.Value.Month == month)
                .ToList();

            var grouped = payments
                .GroupBy(p => p.PaymentDate!.Value.Day)
                .Select(g => new
                {
                    Day = g.Key,
                    Total = g.Sum(p => p.TotalAmount)
                })
                .OrderBy(g => g.Day)
                .ToList();

            if (grouped.Count == 0)
            {
                RevenueChart.Series = new SeriesCollection(); // clear chart
                RevenueChart.AxisX[0].Labels = new List<string>();
                tbThongBao.Visibility = Visibility.Visible;
                return;
            }

            Labels = grouped.Select(g => $"Ngày {g.Day}").ToList();
            var values = grouped.Select(g => (double)g.Total).ToList();

            var culture = CultureInfo.GetCultureInfo("vi-VN");

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = $"Tháng {month}/{year}",
                    Values = new ChartValues<double>(values),
                    DataLabels = true,
                    LabelPoint = point =>
                    {
                        int index = (int)point.X;
                        string dayLabel = Labels[index];
                        double amount = point.Y;

                        string formattedAmount = string.Format(culture, "{0:#,0} VNĐ", amount);
                        return $"{dayLabel}\n{formattedAmount}";
                    }
                }
            };
            tbThongBao.Visibility = Visibility.Collapsed;
            RevenueChart.Series = SeriesCollection;
            RevenueChart.AxisX[0].Labels = Labels;

        }

        private void cbThang_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            LoadFromComboBoxes();
        }

        private void cbNam_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            LoadFromComboBoxes();
        }

        private void LoadFromComboBoxes()
        {
            if (cbThang.SelectedItem != null && cbNam.SelectedItem != null)
            {
                int year = (int)cbNam.SelectedItem;
                int month = (int)cbThang.SelectedItem;
                LoadRevenueByMonth(year, month);
            }
        }


        private void InitMonthYearComboBox()
        {
            cbThang.ItemsSource = Enumerable.Range(1, 12);
            cbThang.SelectedIndex = DateTime.Now.Month - 1;

            int currentYear = DateTime.Now.Year;
            cbNam.ItemsSource = Enumerable.Range(2020, 6 + (currentYear - 2020));
            cbNam.SelectedItem = currentYear;
        }
        private void btn_BackToMain_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close(); // Đóng cửa sổ quản lý phòng
        }
    }
}
