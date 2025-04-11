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
using Microsoft.EntityFrameworkCore.Diagnostics;
using QlKhachSan.Models;

namespace QlKhachSan
{
    /// <summary>
    /// Interaction logic for QLNhanVien.xaml
    /// </summary>
    public partial class QLNhanVien : Window
    {
        private List<Employee> employees;

        public QLNhanVien()
        {
            InitializeComponent();
            LoadData();

        }

        private void LoadData()
        {
            using (var db = new QlksContext())
            {
                employees = db.Employees.ToList();
                dgNhanVien.ItemsSource = employees;
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddNhanVien();
            if (addWindow.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgNhanVien.SelectedItem is Employee selectedEmployee)
            {
                var EditWindow = new EditNhanVien(selectedEmployee);
                if (EditWindow.ShowDialog() == true)
                {
                    LoadData();
                }
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgNhanVien.SelectedItem is Employee selectedEmployee &&
                MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên {selectedEmployee.FullName} không?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                using (var db = new QlksContext())
                {
                    var employeeToDelete = db.Employees.Find(selectedEmployee.EmployeeId);
                    if (employeeToDelete != null)
                    {
                        db.Employees.Remove(employeeToDelete);
                        db.SaveChanges();
                        LoadData();
                    }
                }
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(keyword) || keyword == "Tìm kiếm...")
            {
                dgNhanVien.ItemsSource = employees;
            }
            else
            {
                var filteredEmployees = employees.Where(emp => emp.FullName.ToLower().Contains(keyword)).ToList();
                if (filteredEmployees.Count == 0)
                {
                    dgNhanVien.ItemsSource = employees;                   
                }
                else
                {
                    dgNhanVien.ItemsSource = filteredEmployees;
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
